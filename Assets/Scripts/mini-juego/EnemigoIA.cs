using UnityEngine;

public class EnemigoIA : MonoBehaviour
{
    public enum TipoEnemigo { Fantasma, ZombieVerde, ZombieRojo }
    public TipoEnemigo tipo = TipoEnemigo.ZombieVerde;

    public float velocidad = 3f;
    public GameObject particulasDaño;

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

        // Configurar según tipo
        ConfigurarTipo();
    }

    void ConfigurarTipo()
    {
        Renderer rend = GetComponentInChildren<Renderer>();

        switch (tipo)
        {
            case TipoEnemigo.Fantasma:
                velocidad = 5f; // Más rápido
                if (rend) rend.material.color = new Color(0.8f, 0.8f, 1f); // Azul claro
                break;

            case TipoEnemigo.ZombieVerde:
                velocidad = 2f; // Más lento
                if (rend) rend.material.color = new Color(0, 1f, 0.3f); // Verde
                break;

            case TipoEnemigo.ZombieRojo:
                velocidad = 3.5f; // Normal
                if (rend) rend.material.color = new Color(1f, 0.2f, 0); // Rojo
                break;
        }
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
            AplicarEfecto();

            if (AudioManager.instance != null)
            {
                AudioManager.instance.SonidoDaño();
            }

            if (particulasDaño != null)
            {
                Instantiate(particulasDaño, collision.contacts[0].point, Quaternion.identity);
            }

            // Shake de cámara
            CamaraFollow cam = Camera.main.GetComponent<CamaraFollow>();
            if (cam != null)
            {
                cam.Shake(0.3f, 0.5f);
            }

            // Flash rojo
            if (FlashDano.instance != null)
            {
                FlashDano.instance.MostrarFlash();
            }

            Destroy(gameObject);
        }
    }

    void AplicarEfecto()
    {
        if (GameManager.instance == null) return;

        switch (tipo)
        {
            case TipoEnemigo.Fantasma:
                // Quitar 5 monedas (restar puntos)
                GameManager.instance.GanarPuntos(-50); // -50 puntos = -5 monedas (cada moneda vale 10)
                Debug.Log("Fantasma tocó: -5 monedas (-50 puntos)");
                break;

            case TipoEnemigo.ZombieVerde:
                // Quitar 1 vida
                GameManager.instance.PerderVida(1);
                Debug.Log("Zombie Verde tocó: -1 vida");
                break;

            case TipoEnemigo.ZombieRojo:
                // Quitar 2 vidas
                GameManager.instance.PerderVida(2);
                Debug.Log("Zombie Rojo tocó: -2 vidas");
                break;
        }
    }
}