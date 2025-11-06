using UnityEngine;
using TMPro; // Para el texto de UI

public class GameManager : MonoBehaviour
{
    // Singleton (solo puede haber uno)
    public static GameManager instance;

    // Variables del juego
    public int vidaMaxima = 3;
    public int vidaActual;
    public int puntos = 0;
    public float tiempoRestante = 60f;
    public bool juegoActivo = true;

    // Referencias UI (las conectaremos después)
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI textoTiempo;
    public GameObject panelGameOver;
    public GameObject panelVictoria;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarUI();

        if (panelGameOver) panelGameOver.SetActive(false);
        if (panelVictoria) panelVictoria.SetActive(false);
    }

    void Update()
    {
        if (!juegoActivo) return;

        // Contador de tiempo
        tiempoRestante -= Time.deltaTime;

        if (tiempoRestante <= 0)
        {
            Victoria();
        }

        ActualizarUI();
    }

    public void PerderVida(int cantidad = 1)
    {
        vidaActual -= cantidad;
        Debug.Log("Vida actual: " + vidaActual);

        if (vidaActual <= 0)
        {
            GameOver();
        }

        ActualizarUI();
    }

    public void GanarPuntos(int cantidad)
    {
        puntos += cantidad;
        ActualizarUI();
    }

    void ActualizarUI()
    {
        if (textoVida) textoVida.text = "Vida: " + vidaActual;
        if (textoPuntos) textoPuntos.text = "Puntos: " + puntos;
        if (textoTiempo) textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante) + "s";
    }

    void GameOver()
    {
        juegoActivo = false;
        Debug.Log("GAME OVER");
        if (panelGameOver) panelGameOver.SetActive(true);
        Time.timeScale = 0; // Pausar el juego
    }

    void Victoria()
    {
        juegoActivo = false;
        Debug.Log("¡VICTORIA!");
        if (panelVictoria) panelVictoria.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReiniciarJuego()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void SalirJuego()
    {
        Debug.Log("Saliendo del juego...");

#if UNITY_EDITOR
            // Si estás en el Editor de Unity, para el Play
            UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estás en el juego compilado, cierra la aplicación
        Application.Quit();
#endif
    }
}