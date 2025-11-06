using UnityEngine;

public class ParedReactiva : MonoBehaviour
{
    private Renderer rend;
    private Color colorOriginal;

    void Start()
    {
        rend = GetComponent<Renderer>();
        colorOriginal = rend.material.color;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si el Player choca con la pared
        if (collision.gameObject.CompareTag("Player"))
        {
            rend.material.color = Color.red;
            Debug.Log("¡Player chocó con la pared!");

            // Volver al color original después de 0.5 segundos
            Invoke("RestaurarColor", 0.5f);
        }
    }

    void RestaurarColor()
    {
        rend.material.color = colorOriginal;
    }
}