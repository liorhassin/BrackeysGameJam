using UnityEngine;

public class ElectricityShortageHazard : Hazard
{

    public Interactable box;

    private void Start()
    {
        hazardName = "Electricity Shortage";
        isFixed = true;
        
    }
    public override void TriggerHazard()
    {
        Debug.Log("Electricity Shortage Triggered");
        box.active = true;
    }
    public override void CleanupHazard()
    {
        Debug.Log("Electricity Shortage Cleaned Up");
        box.active = false;
    }
    public override void ApplyFailure()
    {
        Debug.Log("Electricity Shortage Failed");
    }
}
