using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public float intervalo = 3f;
    public float radioSpawn = 12f; // A qué distancia del centro aparecen

    void Start()
    {
        InvokeRepeating("SpawnEnemigo", 2f, intervalo);
    }

    void SpawnEnemigo()
    {
        // Posición aleatoria en el borde de la arena
        float angulo = Random.Range(0f, 360f);
        float x = Mathf.Cos(angulo * Mathf.Deg2Rad) * radioSpawn;
        float z = Mathf.Sin(angulo * Mathf.Deg2Rad) * radioSpawn;
        
        Vector3 posicion = new Vector3(x, 1f, z);
        
        // Crear enemigo
        Instantiate(enemigoPrefab, posicion, Quaternion.identity);
        
        Debug.Log("¡Enemigo spawneado!");
    }
}