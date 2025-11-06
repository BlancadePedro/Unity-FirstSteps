using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public float velocidad = 3f;
    public GameObject particulasDaño; // NUEVO
    
    private Transform jugador;
    private Rigidbody rb;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            jugador = playerObj.transform;
        }
        
        rb = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material.color = new Color(Random.Range(0.7f, 1f), 0, 0);
    }

    void Update()
    {
        if (jugador != null)
        {
            PerseguirJugador();
        }
        
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void PerseguirJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        Vector3 movimiento = new Vector3(direccion.x, 0, direccion.z) * velocidad;
        rb.linearVelocity = new Vector3(movimiento.x, rb.linearVelocity.y, movimiento.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Enemigo tocó al jugador!");
            
            if (GameManager.instance != null)
            {
                GameManager.instance.PerderVida(1);
            }
            
            if (AudioManager.instance != null)
            {
                AudioManager.instance.SonidoDaño();
            }
            
            // NUEVO: Partículas de daño
            if (particulasDaño != null)
            {
                Instantiate(particulasDaño, collision.contacts[0].point, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }
}