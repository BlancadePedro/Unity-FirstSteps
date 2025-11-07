using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movimiento
    public float velocidad = 8f;
    public float fuerzaSalto = 5f;

    // Estado
    private bool estaEnSuelo = true;
    private bool estaAgachado = false;

    // Componentes
    private Rigidbody rb;
    private Renderer rend;
    private Vector3 escalaOriginal;

    // Colores para "animación" visual
    private Color colorIdle = Color.cyan;
    private Color colorRun = Color.yellow;
    private Color colorSalto = Color.magenta;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        Mover();
        Saltar();
        Agacharse();
        ActualizarEstadoVisual();
    }

    void Mover()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moveX, 0, moveZ) * velocidad;
        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);
    }

    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo && !estaAgachado)
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            estaEnSuelo = false;

            // NUEVO: Reproducir sonido
            if (AudioManager.instance != null)
            {
                AudioManager.instance.SonidoSalto();
            }

            Debug.Log("¡Saltó!");
        }
    }

    void Agacharse()
    {
        // Agacharse con Left Control
        if (Input.GetKeyDown(KeyCode.LeftControl) && estaEnSuelo)
        {
            estaAgachado = true;
            transform.localScale = new Vector3(escalaOriginal.x, escalaOriginal.y * 0.5f, escalaOriginal.z);
            velocidad *= 0.5f; // Más lento agachado
            Debug.Log("Agachado");
        }

        // Levantarse al soltar
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            estaAgachado = false;
            transform.localScale = escalaOriginal;
            velocidad *= 2f; // Restaurar velocidad
            Debug.Log("De pie");
        }
    }

    void ActualizarEstadoVisual()
    {
        // Cambiar color según estado (simulación de animación)
        if (!estaEnSuelo)
        {
            rend.material.color = colorSalto; // Magenta en el aire
        }
        else if (rb.linearVelocity.magnitude > 0.1f)
        {
            rend.material.color = colorRun; // Amarillo corriendo
        }
        else
        {
            rend.material.color = colorIdle; // Cyan quieto
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Suelo")
        {
            estaEnSuelo = true;
        }
    }
}