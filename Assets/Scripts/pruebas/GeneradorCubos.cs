using UnityEngine;

public class GeneradorCubos : MonoBehaviour
{
    public int cantidad = 2;     // número de cubos <- TIENE QUE COINCIDIR CON LOS SETTINGS
    public float separacion = 2f; // distancia entre cubos

    void Start()
    {
        // Calcula el punto de inicio (para centrar la fila en el plano)
        float inicioX = -((cantidad - 1) * separacion) / 2f;

        for (int i = 0; i < cantidad; i++)
        {
            // Crear cubo
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Posición centrada
            cubo.transform.position = new Vector3(inicioX + i * separacion, 0.5f, 0);

            // Color aleatorio
            cubo.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            Debug.Log("Cubo " + i + " creado!");
        }
    }
}
