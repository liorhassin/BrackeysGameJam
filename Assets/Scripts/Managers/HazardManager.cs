using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.Linq;

public class HazardManager : MonoBehaviour
{
    public List<Hazard> hazards;
    public float minTimeBetweenHazards;
    public float maxTimeBetweenHazards;
    public GameObject hazardNotificationPanel;
    public TMP_Text hazardNotificationTextTitle;
    public TMP_Text hazardNotificationTextDescription;
    public GameObject Pistol;

    public List<Hazard> fixedHazards = new List<Hazard>();
    public List<int> fixedStartingPoints = new List<int>();
    private float nextHazardStartingProgress;
    private int hazardCounter;
    private Hazard nextHazard;

    private float nextHazardTime;
    public float progress;

    public bool refreshed = true;
    public Dictionary<string, GameObject> hazardObjects;

    void Start()
    {
        /*minTimeBetweenHazards = 20f;
        maxTimeBetweenHazards = 60f;
        nextHazardTime = 20f;

        // Find all hazards and filter out the disabled ones
        hazards.AddRange(FindObjectsByType<Hazard>(FindObjectsSortMode.None).Where(h => h.gameObject.activeInHierarchy).ToList());

        if (hazards.Count == 0)
        {
            Debug.LogWarning("No hazards found in the scene!");
        }*/
        if(fixedHazards.Count == 0)
        {
            Debug.LogWarning("No fixed hazards found in the scene!");
        }
        else{
            nextHazard = fixedHazards[0];
            nextHazardStartingProgress = fixedStartingPoints[0];
            hazardCounter = 0;
        }

        hazardNotificationPanel.SetActive(false);
        Pistol.SetActive(false);
    }


    void Update()
    {
        /*if (Time.time >= nextHazardTime)
        {
            TriggerRandomHazard();
            ScheduleNextHazard();
        }*/
        if(progress >= nextHazardStartingProgress){
            if (refreshed){
                TriggerNextHazard();
                refreshed = false;
            }
            hazardCounter++;
            if(hazardCounter < fixedHazards.Count){
                refreshed = true;
                nextHazard = fixedHazards[hazardCounter];
                nextHazardStartingProgress = fixedStartingPoints[hazardCounter];
            }
        }
        
    }

    private void TriggerNextHazard()
    {

            nextHazard.StartHazard();
            Debug.Log("Hazard Triggered: " + nextHazard.hazardName);
            ShowHazardNotification(nextHazard);
    }
/*
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
            ShowHazardNotification(selectedHazard);
        }
        else
        {
            Debug.Log("No Hazards Available");
            lastTriggeredHazard = null;
        }
    }
*/
    private void ShowHazardNotification(Hazard hazard)
    {
        hazardNotificationTextTitle.text = "Hazard: " + hazard.hazardName;
        hazardNotificationTextDescription.text = hazard.hazardDescription;
        hazardNotificationPanel.SetActive(true);
        StartCoroutine(HideHazardNotification());
    }

    private IEnumerator HideHazardNotification()
    {
        yield return new WaitForSeconds(8f);
        hazardNotificationPanel.SetActive(false);
    }

    public void SetProgress(float percentage)
    {
        progress = Mathf.Clamp(percentage, 0f, 100f);
    }
}
