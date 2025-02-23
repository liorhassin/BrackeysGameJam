using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public GameObject tutorialPanel;
    public TextMeshProUGUI tutorialText;

    private HashSet<string> completedTutorials = new HashSet<string>();
    private Coroutine hideCoroutine; 

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
    }

    void Start(){
        tutorialPanel.SetActive(false); 
    }

    public void ShowTutorial(string tutorialKey, string message)
    {
        if (completedTutorials.Contains(tutorialKey)) return; 

        completedTutorials.Add(tutorialKey);
        tutorialPanel.SetActive(true);
        tutorialText.text = message;

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
