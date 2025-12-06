using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Fuentes de audio")]
    public AudioSource musicSource; // Fuente para música de fondo (recomendado: Loop activado por script)
    public AudioSource sfxSource;   // Fuente para efectos (PlayOneShot)

    [Header("Clips")]
    public AudioClip introMusic;
    public AudioClip menuMusic;
    public AudioClip effectClick;

    [Header("UI")]
    public Slider sliderMusic;
    public Slider sliderSfx;

    private const string PREF_MUSIC_VOL = "AM_MusicVolume";
    private const string PREF_SFX_VOL = "AM_SfxVolume";

    void Awake()
    {
        // Singleton sencillo
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Cargar volúmenes guardados
        float musicVol = PlayerPrefs.GetFloat(PREF_MUSIC_VOL, 1f);
        float sfxVol = PlayerPrefs.GetFloat(PREF_SFX_VOL, 1f);

        if (musicSource != null)
        {
            musicSource.volume = musicVol;
            musicSource.loop = true; // Aseguramos bucle por defecto
        }

        if (sfxSource != null)
        {
            sfxSource.volume = sfxVol;
        }

        if (sliderMusic != null)
        {
            sliderMusic.minValue = 0f;
            sliderMusic.maxValue = 1f;
            sliderMusic.value = musicVol;
            sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        }
        if (sliderSfx != null)
        {
            sliderSfx.minValue = 0f;
            sliderSfx.maxValue = 1f;
            sliderSfx.value = sfxVol;
            sliderSfx.onValueChanged.AddListener(SetSfxVolume);
        }
    }

    void OnDestroy()
    {
        if (sliderMusic != null) sliderMusic.onValueChanged.RemoveListener(SetMusicVolume);
        if (sliderSfx != null) sliderSfx.onValueChanged.RemoveListener(SetSfxVolume);
    }

    // Reproducir música (por defecto en bucle)
    public void PlayMusic(AudioClip clip, bool loop = true, bool restartIfSame = false)
    {
        if (musicSource == null || clip == null) return;

        if (!restartIfSame && musicSource.isPlaying && musicSource.clip == clip) return;

        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    // Parar música
    public void StopMusic()
    {
        if (musicSource == null) return;
        musicSource.Stop();
    }

    // Reproducir efecto
    public void PlayEffect(AudioClip clip = null, float volumeScale = 1f)
    {
        if (sfxSource == null) return;
        var c = clip ?? effectClick;
        if (c == null) return;
        sfxSource.PlayOneShot(c, Mathf.Clamp01(sfxSource.volume * volumeScale));
    }

    // Controladores de UI
    public void SetMusicVolume(float volume)
    {
        if (musicSource != null) musicSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(PREF_MUSIC_VOL, Mathf.Clamp01(volume));
        PlayerPrefs.Save();
    }

    public void SetSfxVolume(float volume)
    {
        if (sfxSource != null) sfxSource.volume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat(PREF_SFX_VOL, Mathf.Clamp01(volume));
        PlayerPrefs.Save();
    }
}
