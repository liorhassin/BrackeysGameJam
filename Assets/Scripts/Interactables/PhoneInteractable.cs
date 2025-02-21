using UnityEngine;

public class PhoneInteractable : Interactable
{
    public override void OnInteract()
    {
        PhoneHazard activeHazard = FindAnyObjectByType<PhoneHazard>();
        
        if (activeHazard != null && !activeHazard.isFixed)
        {
            activeHazard.ResolveHazard();
            Debug.Log("📞 Phone call answered! Hazard resolved.");
        }
        else
        {
            Debug.Log("Nothing to fix here.");
        }
    }
}
