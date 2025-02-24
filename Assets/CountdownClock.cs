using UnityEngine;
using TMPro;

public class CountdownClock : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    private float totalTime = 7.6f * 60;
    public bool active = true;
    [SerializeField] private GameObject uiCanvas;

    void Start()
    {
        active = true;
        if (clockText == null)
        {
            Debug.LogError("⚠️ ClockText is not assigned!");
            return;
        }
        UpdateClockDisplay();
        InvokeRepeating("UpdateClock", 1f, 1f);
    }

    public void Stop(){
        active = false;
        if (totalTime == 0){
            totalTime = 1;
        }
    }

    void UpdateClock()
    {
        if (!active){
            return;
        }
        if (totalTime > 0)
        {
            totalTime--;
            UpdateClockDisplay();
        }
        else
        {
            CancelInvoke("UpdateClock");
            Debug.Log("Time is up!");

            LaptopManager laptopManager = FindFirstObjectByType<LaptopManager>();
            float progress = laptopManager != null ? laptopManager.GetProgress() : 0f;
            EndGameUIManager endGameUI = FindFirstObjectByType<EndGameUIManager>();

            if (endGameUI != null)
            {
                Debug.Log("herer 1");
                laptopManager.DisableTyping();
                endGameUI.ShowEndGameScreen(false, progress);
            }

            Debug.Log("not hgerer");
        }
    }

    void UpdateClockDisplay()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        clockText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
