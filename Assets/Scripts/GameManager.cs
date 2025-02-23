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

    private HazardManager hazardManager;
    private LaptopManager typingManager;
    private HealthSystem playerHealth;
    private EndGameUIManager endGameUIManager;
    void Start()
    {
        hazardManager = hazardManagerGameObject.GetComponent<HazardManager>();
        typingManager = typingManagerGameObject.GetComponent<LaptopManager>();
        playerHealth = playerGameObject.GetComponent<HealthSystem>();
        endGameUIManager = endGameUIGameObject.GetComponent<EndGameUIManager>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        TutorialManager.instance.ShowTutorial("starting", "Go to your laptop and press E to interact with it. \nYou can pause the game anytime using '~'");
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

        if (Mathf.Approximately(progress, 100f))
        {
            SpaceshipController spaceshipController = FindObjectOfType<SpaceshipController>();

            if (spaceshipController != null)
            {
                spaceshipController.StartMoving();
            }
            else
            {
                Debug.LogError("No SpaceshipController found in the scene!");
            }
            /*uiCanvas.SetActive(false);
            endGameUIManager.ShowEndGameScreen(true, progress);*/
        }
    }
}
