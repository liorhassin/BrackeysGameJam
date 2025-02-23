using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource mainMenuSource;
    public AudioSource gameSource;
    public AudioSource sfxSource;

    public static AudioClip hazardResolved;

    private AudioSource currentMusicSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        SetMusicVolume(musicVolume);
        SetSFXVolume(sfxVolume);
    }

    private void StopAllAudioSources()
    {
        mainMenuSource.Stop();
        gameSource.Stop();
        sfxSource.Stop();
    }

    public void PlayMusic(AudioSource source)
    {
        StopAllAudioSources();
        float volume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        volume = Mathf.Clamp(volume, 0f, 1f);

        if (currentMusicSource != null && currentMusicSource.isPlaying)
        {
            currentMusicSource.Stop();
        }

        currentMusicSource = source;
        currentMusicSource.volume = volume;
        currentMusicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        Debug.Log("SetMusicVolume: " + volume);
        if (currentMusicSource != null)
        {
            currentMusicSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        Debug.Log("SetSFXVolume: " + volume);
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");
        if (scene.name == "GameScene")
            PlayMusic(gameSource);
        else
            PlayMusic(mainMenuSource);
    }

    /*
    To use PlaySFX anywhere in the game, follow these steps:

    1. Create an AudioClip variable in your script:
        public AudioClip randomSFX;

    2. Assign an SFX Clip to this variable in the Inspector.

    3. Call the function when you need to play the sound:
        AudioManager.instance.PlaySFX(randomSFX);

    This will play the sound effect and automatically destroy the temporary AudioSource after it finishes.
    */
    public void PlaySFX(AudioClip clip)
    {
        AudioSource tempSFXSource = new GameObject("TempSFXSource").AddComponent<AudioSource>();
        tempSFXSource.clip = clip;
        tempSFXSource.volume = sfxSource.volume;
        tempSFXSource.Play();
        Destroy(tempSFXSource.gameObject, clip.length);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

        public AudioSource GetCurrentMusicSource()
    {
        return currentMusicSource; // Returns the currently playing music
    }
}
