using System;
using UnityEngine;
using UnityEngine.UI;
using Cursor = UnityEngine.Cursor;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject hazardManagerGameObject;
    [SerializeField] private GameObject typingManagerGameObject;
    [SerializeField] private GameObject endGameUIGameObject;
    [SerializeField] private GameObject uiCanvas;
    public CountdownClock countdownClock;

    public SpaceshipHazard spaceshipHazard;

    private HazardManager hazardManager;
    private LaptopManager typingManager;
    private HealthSystem playerHealth;
    public EndGameUIManager endGameUIManager;
    void Start()
    {
        hazardManager = hazardManagerGameObject.GetComponent<HazardManager>();
        typingManager = typingManagerGameObject.GetComponent<LaptopManager>();
        playerHealth = playerGameObject.GetComponent<HealthSystem>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        Invoke("ShowStartingTutorial", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        float progress = typingManager.GetProgress();
        hazardManager.SetProgress(progress);

        if (playerHealth.isDead)
        {
            uiCanvas.SetActive(false);
            endGameUIManager.ShowEndGameScreen(false, progress);
        }

        if (progress >= 99f)
        {
            spaceshipHazard.StartHazard();
            countdownClock.Stop();
            Invoke(nameof(ShowGameOver), 6f);
            
        }
    }

    void ShowGameOver(){
        uiCanvas.SetActive(false);
        endGameUIManager.ShowEndGameScreen(true, 0);
    }

    void ShowStartingTutorial()
    {
        if (TutorialManager.instance != null)
        {
            TutorialManager.instance.ShowTutorial("starting", "Go to your laptop and press E to interact with it. \nYou can pause the game anytime using '~'");
        }
        else
        {
            Debug.LogError("❌ TutorialManager instance is null!");
        }
    }
}
