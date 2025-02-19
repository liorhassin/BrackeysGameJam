using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class HazardManager : MonoBehaviour
{
    public List<Hazard> hazards;
    public float minTimeBetweenHazards;
    public float maxTimeBetweenHazards;
    public GameObject hazardNotificationPanel;
    public TMP_Text hazardNotificationText;

    private float nextHazardTime;
    public float progress;
    private Hazard lastTriggeredHazard = null;

    public Dictionary<string, GameObject> hazardObjects;

    void Start()
    {
        minTimeBetweenHazards = 10f;
        maxTimeBetweenHazards = 30f;
        nextHazardTime = 10f;
        hazards.AddRange(FindObjectsByType<Hazard>(FindObjectsSortMode.None));
        if (hazards.Count == 0)
        {
            Debug.LogWarning("No hazards found in the scene!");
        }
        hazardNotificationPanel.SetActive(false);
        
    }

    void Update()
    {
        if (Time.time >= nextHazardTime)
        {
            TriggerRandomHazard();
            ScheduleNextHazard();
        }
    }

    private void ScheduleNextHazard()
    {
        float timeUntilNextHazard = Random.Range(minTimeBetweenHazards, maxTimeBetweenHazards);
        nextHazardTime = Time.time + timeUntilNextHazard;
        Debug.Log("Next Hazard Time: " + nextHazardTime);
    }

    private void TriggerRandomHazard()
    {
        List<Hazard> availableHazards = hazards.FindAll(h => h.difficultyLevel <= progress && h != lastTriggeredHazard);

        if (availableHazards.Count > 0)
        {
            Hazard selectedHazard = availableHazards[Random.Range(0, availableHazards.Count)];
            selectedHazard.StartHazard();
            Debug.Log("Hazard Triggered: " + selectedHazard.hazardName);
            lastTriggeredHazard = selectedHazard;
            ShowHazardNotification(selectedHazard.hazardName);
        }
        else
        {
            Debug.Log("No Hazards Available");
            lastTriggeredHazard = null;
        }
    }

    private void ShowHazardNotification(string hazardName)
    {
        hazardNotificationText.text = "Hazard: " + hazardName;
        hazardNotificationPanel.SetActive(true);
        StartCoroutine(HideHazardNotification());
    }

    private IEnumerator HideHazardNotification()
    {
        yield return new WaitForSeconds(5f);
        hazardNotificationPanel.SetActive(false);
    }

    public void SetProgress(float percentage)
    {
        progress = Mathf.Clamp(percentage, 0f, 100f);
    }
}
