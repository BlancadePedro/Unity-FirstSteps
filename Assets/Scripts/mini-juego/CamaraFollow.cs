using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform objetivo;
    public Vector3 offset = new Vector3(0, 6, -6); // Más cerca para ver el shake
    public float suavidad = 5f;
    public bool mirarAlObjetivo = true;

    // Variables del shake
    private float shakeDuracion = 0f;
    private float shakeMagnitud = 0.3f;
    private float shakeDampening = 1.0f;
    private Vector3 posicionOriginal;

    void Start()
    {
        if (objetivo == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                objetivo = player.transform;
            }
            else
            {
                Debug.LogError("¡No se encontró el Player!");
            }
        }

        if (objetivo != null)
        {
            offset = transform.position - objetivo.position;
        }
    }

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 posicionDeseada = objetivo.position + offset;

        // Aplicar shake
        if (shakeDuracion > 0)
        {
            posicionDeseada += Random.insideUnitSphere * shakeMagnitud;
            shakeDuracion -= Time.deltaTime * shakeDampening;
        }

        // Mover cámara suavemente
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavidad * Time.deltaTime);

        // Mirar al jugador
        if (mirarAlObjetivo)
        {
            transform.LookAt(objetivo.position + Vector3.up * 1f);
        }
    }

    // Función para activar el shake
    public void Shake(float duracion, float magnitud)
    {
        shakeDuracion = duracion;
        shakeMagnitud = magnitud;
    }
}