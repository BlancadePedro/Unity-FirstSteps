using UnityEngine;

public class SpawnerMonedas : MonoBehaviour
{
    public GameObject monedaPrefab;
    public float intervalo = 5f;
    public int monedasMaximas = 5; // Máximo de monedas en escena
    private int monedasActuales = 0;

    void Start()
    {
        InvokeRepeating("SpawnMoneda", 2f, intervalo);
    }

    void SpawnMoneda()
    {
        // No spawnar si ya hay muchas monedas
        if (monedasActuales >= monedasMaximas) return;

        // Posición aleatoria en la arena (evitando los bordes)
        float x = Random.Range(-12f, 12f);
        float z = Random.Range(-12f, 12f);
        Vector3 posicion = new Vector3(x, 0.5f, z);

        // Crear moneda
        GameObject nuevaMoneda = Instantiate(monedaPrefab, posicion, Quaternion.identity);
        monedasActuales++;

        // Suscribirse al evento de destrucción para actualizar el contador
        MonedaCounter counter = nuevaMoneda.AddComponent<MonedaCounter>();
        counter.spawner = this;

        Debug.Log("Moneda spawneada en " + posicion);
    }

    public void MonedaRecogida()
    {
        monedasActuales--;
    }
}

// Script auxiliar para contar cuando se destruye una moneda
public class MonedaCounter : MonoBehaviour
{
    public SpawnerMonedas spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.MonedaRecogida();
        }
    }
}