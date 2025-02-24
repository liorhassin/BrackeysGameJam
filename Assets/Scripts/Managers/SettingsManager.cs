using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public Slider soundSlider;
    public Slider sensitivitySlider;
    public Slider musicSlider;
    public AudioManager audioManager;
    public PlayerCamera playerCamera;

    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>(); // Faster lookup
        LoadSettings();

        if (audioManager != null)
        {
            Debug.Log("Found AudioManager: " + audioManager.gameObject.name);
        }
        else
        {
            Debug.LogWarning("AudioManager not found in the scene!");
        }
        playerCamera = FindAnyObjectByType<PlayerCamera>();
    }

    private void OnEnable()
    {
        LoadSettings();
    }

    public void SetSoundVolume()
    {
        AudioListener.volume = soundSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        if (audioManager != null)
        {
            audioManager.SetSFXVolume(soundSlider.value);
        }

    }

    public void SetSensitivity()
    {
        float sen = sensitivitySlider.value;
        PlayerPrefs.SetFloat("Sensitivity", sen);
        if (playerCamera != null)
        {
            PlayerController.SetSensitivity(sen);
        }
        
    }

    public void SetMusicVolume()
    {
        AudioListener.volume = musicSlider.value;
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        if (audioManager != null)
        {
            audioManager.SetMusicVolume(musicSlider.value);
        }
    }

    public void LoadSettings()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        SetSoundVolume();
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 5f);
        SetSensitivity();
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SetMusicVolume();
    }
}
