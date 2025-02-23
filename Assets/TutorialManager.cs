using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Make sure to include this
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public GameObject tutorialPanelPrefab; // Assign in Inspector
    private GameObject tutorialPanel; // Keep private
    public Canvas UiCanvas; // Assign the main UI Canvas in Inspector

    private HashSet<string> completedTutorials = new HashSet<string>();
    private Coroutine hideCoroutine;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Ensure this object persists across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instance if it exists
            return;
        }

        completedTutorials = new HashSet<string>();
        hideCoroutine = null;

        if (tutorialPanelPrefab != null && UiCanvas != null)
        {
            tutorialPanel = Instantiate(tutorialPanelPrefab, UiCanvas.transform);
            tutorialPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("TutorialManager: Missing tutorialPanelPrefab or UiCanvas in the Inspector.");
        }

        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Unsubscribe from the scene loaded event to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenuScene")
        {
            // Destroy this object when the main menu is loaded
            Destroy(gameObject);
        }
    }

    public void ShowTutorial(string tutorialKey, string message)
    {
        if (completedTutorials.Contains(tutorialKey)) return;

        completedTutorials.Add(tutorialKey);

        if (tutorialPanel == null)
        {
            Debug.LogError("TutorialManager: tutorialPanel is null. Check if tutorialPanelPrefab is assigned.");
            return;
        }

        TextMeshProUGUI tutorialText = tutorialPanel.GetComponentInChildren<TextMeshProUGUI>();

        if (tutorialText != null)
        {
            tutorialText.text = message; // Set the message on the TextMeshProUGUI component
        }
        else
        {
            Debug.LogError("TutorialManager: No TextMeshProUGUI found in the tutorialPanel.");
        }

        tutorialPanel.SetActive(true);

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideTutorialAfterDelay(5f));
    }

    private IEnumerator HideTutorialAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        tutorialPanel.SetActive(false);
    }
}
