using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour {

    public static EndGameUIManager instance;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI progressPercentage;
    public TextMeshProUGUI pp;
    public TextMeshProUGUI msg;
    public Canvas uiCanvas;

    public GameObject pauseMenu;
    public Button mainMenuButton;
    public EndGame endGameScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         mainMenuButton.onClick.AddListener(GoToMainMenu);

        endGameScreen.gameObject.SetActive(false);
    }

    public void ShowEndGameScreen(bool hasWon, float percentage) {
        titleText.text = hasWon ? "You Won?" : "You Got Expelled From University";
        if (hasWon){
            msg.text = "Aliens took you right before you could assign the homework but at least you're out to an adventure";
            progressPercentage.enabled = false;
            pp.enabled = false;
        }
        else{
            progressPercentage.text = percentage + "%";
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        if (endGameScreen == null){
            endGameScreen = FindFirstObjectByType<EndGame>();
        }
        uiCanvas.gameObject.SetActive(true);
        uiCanvas.enabled = true;
        endGameScreen.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
    }

    private void HideEndGameScreen() {
        endGameScreen.gameObject.SetActive(false);
    }

    private void GoToMainMenu() {
        SceneManager.LoadScene("MainMenuScene");
    }
}
