using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashDano : MonoBehaviour
{
    public static FlashDano instance;
    private Image imagen;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        imagen = GetComponent<Image>();

        // NUEVO: Asegurarse de que el objeto padre esté activo
        gameObject.SetActive(true);
    }

    void Start()
    {
        // Empezar invisible
        Color c = imagen.color;
        c.a = 0;
        imagen.color = c;
    }

    public void MostrarFlash()
    {
        // NUEVO: Activar antes de iniciar la coroutine
        gameObject.SetActive(true);
        StopAllCoroutines(); // Detener cualquier animación anterior
        StartCoroutine(AnimarFlash());
    }

    IEnumerator AnimarFlash()
    {
        Color c = imagen.color;

        // Fade in rápido (0 → 0.5 alpha)
        float duracionFadeIn = 0.1f;
        for (float t = 0; t < duracionFadeIn; t += Time.unscaledDeltaTime)
        {
            c.a = Mathf.Lerp(0, 0.5f, t / duracionFadeIn);
            imagen.color = c;
            yield return null;
        }

        // Fade out (0.5 → 0 alpha)
        float duracionFadeOut = 0.4f;
        for (float t = 0; t < duracionFadeOut; t += Time.unscaledDeltaTime)
        {
            c.a = Mathf.Lerp(0.5f, 0, t / duracionFadeOut);
            imagen.color = c;
            yield return null;
        }

        // Asegurar que termina en 0
        c.a = 0;
        imagen.color = c;
    }
}