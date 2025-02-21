using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    public string hazardName;
    public int difficultyLevel;
    public int hazardDuration;
    public bool isFixed = true;
    public GameObject[] itemsToHighlightGreen;
    public GameObject[] itemsToHighlightRed;

    public HighlightManager highlightManager;

    private void Awake()
    {
        // Get the instance of the HighlightManager from the scene
        highlightManager = HighlightManager.Instance;
    }

    public void StartHazard()
    {
        TriggerHazard();
        isFixed = false;

        // Highlight items at the start of the hazard
        HighlightObjects();

        Invoke(nameof(CheckFailure), hazardDuration);
    }

    private void HighlightObjects()
    {
        // Iterate through the white-highlight array and call the highlight function
        foreach (GameObject item in itemsToHighlightGreen)
        {
            highlightManager.HighlightObject(item, Color.green);
        }

        // Iterate through the red-highlight array and call the highlight function
        foreach (GameObject item in itemsToHighlightRed)
        {
            highlightManager.HighlightObjectRed(item);
        }
    }

    private void DisableHighlights()
    {
        // Disable highlights for each object after hazard duration
        foreach (GameObject item in itemsToHighlightGreen)
        {
            highlightManager.DisableHighlight(item);
        }

        foreach (GameObject item in itemsToHighlightRed)
        {
            highlightManager.DisableHighlight(item);
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
            isFixed = true;
        }
        CleanupHazard();

        // Disable highlights when hazard finishes
        DisableHighlights();
    }

    public void ResolveHazard()
    {
        isFixed = true;
        CleanupHazard();
        Debug.Log("Hazard Resolved");

        // Disable highlights when hazard is resolved
        DisableHighlights();
    }
}
