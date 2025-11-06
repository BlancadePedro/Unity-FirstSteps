using UnityEngine;

public class DestruirDespuesDeTiempo : MonoBehaviour
{
    public float tiempoDeVida = 2f;

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }
}