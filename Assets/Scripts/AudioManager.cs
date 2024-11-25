using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;  // Instancia singleton
    public AudioSource musicSource;      // Fuente para m�sica
    public AudioSource effectsSource;    // Fuente para efectos de sonido

    public AudioClip[] musicClips;      // Clips de m�sica
    public AudioClip[] effectsClips;    // Clips de efectos de sonido

    void Awake()
    {
        // Asegurarse de que solo haya una instancia del AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // No destruir al cambiar de escena
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // M�todo para reproducir m�sica
    public void PlayMusic(int index)
    {
        if (musicClips.Length > 0 && index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.Play();
        }
    }

    // M�todo para reproducir efectos de sonido
    public void PlaySoundEffect(int index)
    {
        if (effectsClips.Length > 0 && index >= 0 && index < effectsClips.Length)
        {
            effectsSource.PlayOneShot(effectsClips[index]);
        }
    }

    // M�todo para detener m�sica
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // M�todo para detener efectos de sonido
    public void StopSoundEffects()
    {
        effectsSource.Stop();
    }
}
