using UnityEngine;

public class Fire : Interactable
{
    public FireHazard hazard;

    public override void OnInteract()
    {
        FireExtinguisherInteractable extinguisher = FindAnyObjectByType<FireExtinguisherInteractable>();

        if (extinguisher != null && extinguisher.IsCarried())
        {
            ExtinguishFire();
            extinguisher.Drop(); // Return extinguisher after use
        }
        else
        {
            Debug.Log("You need a fire extinguisher!");
        }
    }

    private void ExtinguishFire()
    {
        Debug.Log("Fire Extinguished!");

        if (hazard != null)
        {
            hazard.ResolveHazard(); // Updated method call
        }
        else
        {
            Debug.LogWarning("HazardManager is not assigned!");
        }

        gameObject.SetActive(false);
         // Removes the fire from the scene
    }
}
