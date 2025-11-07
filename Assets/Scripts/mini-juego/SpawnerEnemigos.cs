using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Prefabs de Enemigos")]
    public GameObject fantasmaPrefab;
    public GameObject zombieVerdePrefab;
    public GameObject zombieRojoPrefab;

    [Header("Spawn")]
    public float intervalo = 3f;
    public float radioSpawn = 12f;

    // Probabilidades (suman 1.0)
    [Range(0f,1f)] public float pFantasma = 0.33f;
    [Range(0f,1f)] public float pZombieVerde = 0.34f; // el resto será rojo

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemigo), 2f, intervalo);
    }

    void SpawnEnemigo()
    {
        // Posición en círculo
        float ang = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 pos = new Vector3(Mathf.Cos(ang) * radioSpawn, 1f, Mathf.Sin(ang) * radioSpawn);

        // Ruleta de probabilidad
        float r = Random.value;
        GameObject prefab = null;
        if (r < pFantasma) prefab = fantasmaPrefab;
        else if (r < pFantasma + pZombieVerde) prefab = zombieVerdePrefab;
        else prefab = zombieRojoPrefab;

        if (prefab != null) Instantiate(prefab, pos, Quaternion.identity);
    }
}
