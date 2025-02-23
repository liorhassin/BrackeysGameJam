using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameUIManager : MonoBehaviour {

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI progressPercentage;

    public Button mainMenuButton;
    public GameObject endGameScreen;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        endGameScreen.SetActive(false);
    }

    public void ShowEndGameScreen(bool hasWon, float percentage) {
        titleText.text = hasWon ? "You Won!" : "You Lost!";
        progressPercentage.text = percentage + "%";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        endGameScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private void HideEndGameScreen() {
        gameObject.SetActive(false);
    }

    private void GoToMainMenu() {
        HideEndGameScreen();
        SceneManager.LoadScene("MainMenuScene");
    }
}
