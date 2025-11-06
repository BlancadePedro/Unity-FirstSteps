using UnityEngine;

public class Moneda : MonoBehaviour
{
    public int valorPuntos = 10;
    public float velocidadRotacion = 100f;
    public GameObject particulasMoneda; // NUEVO: Prefab de partículas

    void Update()
    {
        transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar puntos
            if (GameManager.instance != null)
            {
                GameManager.instance.GanarPuntos(valorPuntos);
            }

            // Reproducir sonido
            if (AudioManager.instance != null)
            {
                AudioManager.instance.SonidoMoneda();
            }

            // NUEVO: Instanciar partículas
            if (particulasMoneda != null)
            {
                Instantiate(particulasMoneda, transform.position, Quaternion.identity);
            }

            Debug.Log("¡Moneda recogida! +" + valorPuntos + " puntos");

            Destroy(gameObject);
        }
    }
}