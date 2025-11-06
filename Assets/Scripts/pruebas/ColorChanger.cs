using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Renderer rend;
    public float intervalo = 2f; // Cada cuántos segundos cambia

    void Start()
    {
        rend = GetComponent<Renderer>();

        // Repetir la función CambiarColor cada X segundos
        InvokeRepeating("CambiarColor", 0f, intervalo);
    }

    void CambiarColor()
    {
        // Color aleatorio
        Color nuevoColor = new Color(Random.value, Random.value, Random.value);
        rend.material.color = nuevoColor;

        Debug.Log("¡Color cambiado!");
    }
}