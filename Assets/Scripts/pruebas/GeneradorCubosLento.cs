using UnityEngine;
using System.Collections;

public class GeneradorCubosLento : MonoBehaviour
{
    public int cantidad = 10;          // número de cubos
    public float separacion = 2f;      // distancia entre cubos
    public float retardo = 0.5f;       // segundos entre cada cubo

    void Start()
    {
        StartCoroutine(CrearCubosConDelay());
    }

    IEnumerator CrearCubosConDelay()
    {
        // Calcular punto inicial para centrar los cubos
        float inicioX = -((cantidad - 1) * separacion) / 2f;

        for (int i = 0; i < cantidad; i++)
        {
            GameObject cubo = GameObject.CreatePrimitive(PrimitiveType.Cube);

            // Posición centrada
            cubo.transform.position = new Vector3(inicioX + i * separacion, 0.5f, 2);

            // Color aleatorio
            cubo.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);

            Debug.Log("Cubo " + i + " creado en coroutine!");

            // Esperar antes de crear el siguiente
            yield return new WaitForSeconds(retardo);
        }

        Debug.Log("¡Todos los cubos creados!");
    }
}