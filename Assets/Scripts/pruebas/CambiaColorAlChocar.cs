using UnityEngine;

public class CambiaColorAlChocar : MonoBehaviour
{
    private Renderer rend;
    private bool yaChoco = false; // Para que solo cambie una vez

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si choca con algo llamado "Plane" (el suelo)
        if (collision.gameObject.name == "Plane" && !yaChoco)
        {
            // Cambiar a color aleatorio
            rend.material.color = new Color(Random.value, Random.value, Random.value);
            yaChoco = true;
            
            Debug.Log("¡Esfera tocó el suelo y cambió de color!");
        }
    }
}