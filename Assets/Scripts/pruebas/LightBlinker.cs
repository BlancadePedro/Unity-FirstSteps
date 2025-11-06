using UnityEngine;

public class LuzParpadeo : MonoBehaviour
{
    private Light luzComponente;
    public float intervalo = 0.5f;

    void Start()
    {
        // NUEVO: Verificar si hay Light antes de usarlo
        luzComponente = GetComponent<Light>();

        if (luzComponente != null)
        {
            InvokeRepeating("AlternarLuz", 0f, intervalo);
        }
        else
        {
            Debug.LogWarning("Este objeto no tiene componente Light. Script LuzParpadeo desactivado.");
        }
    }

    void AlternarLuz()
    {
        if (luzComponente != null)
        {
            luzComponente.enabled = !luzComponente.enabled;
        }
    }
}