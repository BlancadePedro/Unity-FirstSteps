using UnityEngine;

public class SpawnerEsferas : MonoBehaviour
{
    public GameObject esferaPrefab;
    public float intervalo = 2f;

    void Start()
    {
        InvokeRepeating("SpawnEsfera", 1f, intervalo);
    }

    void SpawnEsfera()
    {
        Vector3 posicionAleatoria = transform.position + new Vector3(
            Random.Range(-5f, 5f), // Entre 0 y 10,
            0,
            Random.Range(-5f, 5f)
        );

        Instantiate(esferaPrefab, posicionAleatoria, Quaternion.identity);

        Debug.Log("¡Esfera spawneada!");
    }
}
