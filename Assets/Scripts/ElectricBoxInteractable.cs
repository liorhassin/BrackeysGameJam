using UnityEngine;

public class ElectricBoxInteractable : Interactable
{
    public override void OnInteract()
    {
        ElectricityShortageHazard activeHazard = FindAnyObjectByType<ElectricityShortageHazard>();
        if (!activeHazard.isFixed)
        {
            activeHazard.ResolveHazard();
            Debug.Log("Electric Box fixed the Electricity Shortage!");
        }
        else
        {
            Debug.Log("Nothing to fix here.");
        }
    }
}
