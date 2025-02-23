using UnityEngine;
using TMPro;

public class CountdownClock : MonoBehaviour
{
    public TextMeshProUGUI clockText;
    private float totalTime = 12f * 60;

    void Start()
    {
        if (clockText == null)
        {
            Debug.LogError("⚠️ ClockText is not assigned!");
            return;
        }
        UpdateClockDisplay();
        InvokeRepeating("UpdateClock", 1f, 1f);
    }

    void UpdateClock()
    {
        if (totalTime > 0)
        {
            totalTime--;
            UpdateClockDisplay();
        }
        else
        {
            CancelInvoke("UpdateClock");
            Debug.Log("Time is up!");

            LaptopManager laptopManager = FindObjectOfType<LaptopManager>();
            float progress = laptopManager != null ? laptopManager.GetProgress() : 0f;
            EndGameUIManager endGameUI = FindObjectOfType<EndGameUIManager>();

            if (endGameUI != null)
            {
                endGameUI.ShowEndGameScreen(false, progress);
            }
            else
            {
                Debug.LogError("EndGameUIManager not found in the scene!");
            }
        }
    }

    void UpdateClockDisplay()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        clockText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
}
