using UnityEngine;

public class ElectricityShortageHazard : Hazard
{
    private void Start()
    {
        hazardName = "Electricity Shortage";
        hazardDescription = "The electricity has been cut off. Fix it before it's too late!";
        difficultyLevel = 0;
        hazardDuration = 15;
        isFixed = true;
        
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
