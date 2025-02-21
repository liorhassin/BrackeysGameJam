using UnityEngine;

public class FireHazard : Hazard
{
    public GameObject fire;
    private void Start()
    {
        hazardName = "House on Fire";
        difficultyLevel = 10;
        hazardDuration = 15;
        isFixed = true;
        fire.SetActive(false);
    }

    public override void TriggerHazard()
    {
        fire.SetActive(true);

        Debug.Log("The house is on fire! You should put it out.");
    }

    public override void CleanupHazard()
    {
        fire.SetActive(false);
        Debug.Log("The fire has been extinguished.");
    }

    public override void ApplyFailure()
    {
        Debug.Log("The fire spread! You lost the game.");
    }
}
