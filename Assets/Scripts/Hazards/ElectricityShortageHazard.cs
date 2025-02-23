using UnityEngine;

public class ElectricityShortageHazard : Hazard
{

    public ElectricBoxInteractable electricBoxInteractable;

    private void Start()
    {
        hazardName = "Electricity Shortage";
        hazardDescription = "Find the electric switch! Your PC needs power!";

        isFixed = false;
        
    }
    public override void TriggerHazard()
    {
        Debug.Log("Electricity Shortage Triggered");
        electricBoxInteractable.active = true;
    }
    public override void CleanupHazard()
    {
        Debug.Log("Electricity Shortage Cleaned Up");
        electricBoxInteractable.active = false;
    }
    public override void ApplyFailure()
    {
        Debug.Log("Electricity Shortage Failed");
    }
}
