using UnityEngine;

public class ElectricityShortageHazard : Hazard
{

    void Start()
    {
        hazardName = "Electricity Shortage";
        difficultyLevel = 30;
        hazardDuration = 15;
        isFixed = false;
    }
    public override void TriggerHazard()
    {
        Debug.Log("Electricity Shortage Triggered");
    }
    public override void CleanupHazard()
    {
        Debug.Log("Electricity Shortage Cleaned Up");
    }
    public override void ApplyFailure()
    {
        Debug.Log("Electricity Shortage Failed");
    }
}
