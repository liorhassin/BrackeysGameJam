using UnityEngine;

public class SpaceshipHazard : Hazard
{
    public void Start()
    {
        hazardName = "????";
        hazardDescription = "𐰚 𐰘𐰀𐰤𐰆𐰯𐰀𐰴 𐰢𐰆𐰴𐰀𐰺 𐰇𐰠𐰆𐰼𐰀 𐰴𐰀𐰢𐰆𐰺 𐰜𐰇𐰤𐰆𐰺!";
    }

    public override void ApplyFailure()
    {
        throw new System.NotImplementedException();
    }

    public override void CleanupHazard()
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerHazard()
    {
        SpaceshipController spaceshipController = FindFirstObjectByType<SpaceshipController>();

        if (spaceshipController != null)
        {
            spaceshipController.StartMoving();
        }
        else
        {
            Debug.LogError("No SpaceshipController found in the scene!");
        }
    }
}
