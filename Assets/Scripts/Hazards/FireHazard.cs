using UnityEngine;
using System.Collections.Generic;

public class FireHazard : Hazard
{
    public List<Fire> fires; // Ensure list is initialized
    public Transform FireObjects; // Parent object containing all fire instances
    public ExtingItem extingItem;
    public FireExtinguisherInteractable fireExtinguisherInteractable;

    private int currFires;

    private void Start()
    {
        hazardName = "House on Fire";

        Debug.Log("num of fires = " + fires.Count);

        foreach (Fire fire in fires){
            fire.Deactivate();
        }

        fireExtinguisherInteractable.active = false;
    }

    public override void TriggerHazard()
    {
        Debug.Log("The house is on fire! You should put it out.");
        isFixed = false;

        fireExtinguisherInteractable.gameObject.SetActive(true);
        fireExtinguisherInteractable.active = true;

        foreach (Fire fire in fires)
        {
            fire.gameObject.SetActive(true);
            fire.Activate();
        }

        currFires = fires.Count;
    }

    public override void CleanupHazard()
    {
        Debug.Log("The fire has been extinguished.");
        foreach (Fire fire in fires)
        {
            fire.Deactivate();
        }

        extingItem.gameObject.SetActive(false);
    }

    public override void ApplyFailure()
    {
        Debug.Log("The fire spread! You lost the game.");
    }

    public void onFireDead()
    {
        currFires--;
        if (currFires <= 0)
        {
            ResolveHazard();
        }
    }
}
