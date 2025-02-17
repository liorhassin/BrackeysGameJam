using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    public string hazardName;
    public int difficultyLevel;
    public int hazardDuration;
    protected bool isFixed;

    public void StartHazard()
    {
        if (!isFixed)
        {
            TriggerHazard();
            Invoke(nameof(CheckFailure), hazardDuration);
        }
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
