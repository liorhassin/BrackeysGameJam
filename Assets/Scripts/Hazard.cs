using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    public string hazardName;
    public int difficultyLevel;
    public int hazardDuration;
    public bool isFixed = true;

    public void StartHazard()
    {
        TriggerHazard();
        isFixed = false;
        Invoke(nameof(CheckFailure), hazardDuration);
    }


    public abstract void TriggerHazard();
    public abstract void CleanupHazard();
    public abstract void ApplyFailure();

    private void CheckFailure()
    {
        if (!isFixed)
        {
            ApplyFailure();
            Debug.Log("Hazard Failed");
        }
        CleanupHazard();

    }

    public void ResolveHazard()
    {
        isFixed = true;
        CleanupHazard();
        Debug.Log("Hazard Resolved");
    }
}
