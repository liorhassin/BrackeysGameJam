using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour {

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI progressPercentage;

    public Button restartButton;
    public Button mainMenuButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void ShowEndGameScreen(bool hasWon, float percentage) {
        titleText.text = hasWon ? "You Won!" : "You Lost!";
        progressPercentage.text = percentage + "%";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }

    private void HideEndGameScreen() {
        gameObject.SetActive(false);
    }

    private void RestartGame() {
        HideEndGameScreen();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToMainMenu() {
        HideEndGameScreen();
        SceneManager.LoadScene("MainMenuScene");
    }
}
