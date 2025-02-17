using UnityEngine;

public class FireHazard : Hazard
{
    private void Start()
    {
        hazardName = "House on Fire";
        difficultyLevel = 0; 
        hazardDuration = 15;
        isFixed = false;
    }

    public override void TriggerHazard()
    {
        Debug.Log("The house is on fire! You should put it out.");
    }

    public override void CleanupHazard()
    {
        Debug.Log("The fire has been extinguished.");
    }

    public override void ApplyFailure()
    {
        Debug.Log("The fire spread! You lost the game.");
    }
}
