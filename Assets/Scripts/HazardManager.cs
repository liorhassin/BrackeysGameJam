using UnityEngine;
using System.Collections.Generic;

public class HazardManager : MonoBehaviour
{
    public List<Hazard> hazards;
    public float minTimeBetweenHazards;
    public float maxTimeBetweenHazards;

    private float nextHazardTime;
    public float progress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minTimeBetweenHazards = 10f;
        maxTimeBetweenHazards = 30f;
        nextHazardTime = 10f;
        hazards.AddRange(FindObjectsOfType<Hazard>());
        
    }

    // Update is called once per frame
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
        List<Hazard> availableHazards = hazards.FindAll(h => h.difficultyLevel <= progress);

        if (availableHazards.Count > 0)
        {
            Hazard selectedHazard = availableHazards[Random.Range(0, availableHazards.Count)];
            selectedHazard.StartHazard();
            Debug.Log("Hazard Triggered: " + selectedHazard.hazardName);
        }
        else
        {
            Debug.Log("No Hazards Available");
        }
    }

    public void SetProgress(float percentage)
    {
        progress = Mathf.Clamp(percentage, 0f, 100f);
    }
}
