using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform objetivo; // El Player
    public Vector3 offset = new Vector3(0, 15, -10); // Distancia de la cámara
    public float suavidad = 5f; // Qué tan suave sigue (menor = más suave)
    public bool mirarAlObjetivo = true; // Si debe mirar siempre al jugador

    void Start()
    {
        // Si no se asignó objetivo, buscar al Player automáticamente
        if (objetivo == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                objetivo = player.transform;
            }
            else
            {
                Debug.LogError("¡No se encontró el Player! Asegúrate de que tenga el Tag 'Player'");
            }
        }
        
        // Calcular el offset inicial basado en la posición actual
        if (objetivo != null)
        {
            offset = transform.position - objetivo.position;
        }
    }

    void LateUpdate()
    {
        if (objetivo == null) return;
        
        // Calcular la posición deseada
        Vector3 posicionDeseada = objetivo.position + offset;
        
        // Mover suavemente hacia esa posición
        transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavidad * Time.deltaTime);
        
        // Opcional: Siempre mirar al jugador
        if (mirarAlObjetivo)
        {
            transform.LookAt(objetivo.position + Vector3.up * 1f); // +1 en Y para mirar al centro del personaje
        }
    }
}
