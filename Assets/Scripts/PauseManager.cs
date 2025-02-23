using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resumeButton;
    public Button mainMenuButton;

    private bool isPaused = false;
    private AudioSource currentMusic;

    void Start()
    {
        pauseMenuUI.SetActive(false); // Hide pause menu at start
        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        if (AudioManager.instance != null)
        {
            currentMusic = AudioManager.instance.GetCurrentMusicSource();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        if (currentMusic != null && currentMusic.isPlaying)
        {
            currentMusic.Pause(); 
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        if (currentMusic != null)
        {
            currentMusic.UnPause(); 
        }
    }

    private void GoToMainMenu()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MainMenuScene");
    }
}
