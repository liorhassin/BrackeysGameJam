using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public Slider soundSlider;
    public Slider sensitivitySlider;
    public Slider musicSlider;

    private void Start()
    {
        LoadSettings();
    }

    private void OnEnable()
    {
        LoadSettings();
    }

    public void SetSoundVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }

    public void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void LoadSettings()
    {
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", 5f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }
}
