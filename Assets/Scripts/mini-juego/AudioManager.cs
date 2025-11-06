using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton
    public static AudioManager instance;
    
    // Clips de audio (los conectaremos desde el Inspector)
    public AudioClip sonidoSalto;
    public AudioClip sonidoMoneda;
    public AudioClip sonidoDaño;
    public AudioClip musicaFondo;
    
    // Audio Source para efectos (se crea dinámicamente)
    private AudioSource audioSourceEfectos;
    
    // Audio Source para música (lo usaremos del componente)
    private AudioSource audioSourceMusica;

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        // Configurar audio sources
        audioSourceMusica = GetComponent<AudioSource>();
        
        // Crear un segundo Audio Source para efectos
        audioSourceEfectos = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // Reproducir música de fondo en loop
        if (musicaFondo != null && audioSourceMusica != null)
        {
            audioSourceMusica.clip = musicaFondo;
            audioSourceMusica.loop = true;
            audioSourceMusica.volume = 0.3f; // Volumen bajo para música de fondo
            audioSourceMusica.Play();
        }
    }

    // Función para reproducir efectos de sonido
    public void ReproducirSonido(AudioClip clip, float volumen = 1f)
    {
        if (clip != null && audioSourceEfectos != null)
        {
            audioSourceEfectos.PlayOneShot(clip, volumen);
        }
    }
    
    // Funciones específicas para cada sonido (más fácil de usar)
    public void SonidoSalto()
    {
        ReproducirSonido(sonidoSalto, 0.7f);
    }
    
    public void SonidoMoneda()
    {
        ReproducirSonido(sonidoMoneda, 0.8f);
    }
    
    public void SonidoDaño()
    {
        ReproducirSonido(sonidoDaño, 0.9f);
    }
}