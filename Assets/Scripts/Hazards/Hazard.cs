using UnityEngine;

public abstract class Hazard : MonoBehaviour
{
    public string hazardName;
    public string hazardDescription;
    public int difficultyLevel;
    public int hazardDuration;
    public bool isFixed = true;
    public GameObject[] itemsToHighlightGreen;
    public GameObject[] itemsToHighlightRed;

    public AudioClip startAudioClip; // Assign this in the inspector or load via code
    public AudioClip resolvedAudio;
    public AudioSource audioSource;



    public HighlightManager highlightManager;
    private LaptopInteractable laptopInteractable;

    public void Start()
    {
        // Load the AudioClip from Resources folder
        if (resolvedAudio == null){
            resolvedAudio = Resources.Load<AudioClip>("Assets/Sounds/level_up.mp3");
        }
    }

    public void StartHazard()
    {
        if (highlightManager == null){
            highlightManager = FindFirstObjectByType<HighlightManager>();
        }
        if (laptopInteractable == null){
            laptopInteractable = FindFirstObjectByType<LaptopInteractable>();
        }
        laptopInteractable.active = false;

        if (startAudioClip != null){
            audioSource.PlayOneShot(startAudioClip);
        }

        TriggerHazard();
        isFixed = false;
        

        // Highlight items at the start of the hazard
        HighlightObjects();
    }

    public void HighlightObjects()
    {
        // Iterate through the white-highlight array and call the highlight function
        foreach (GameObject item in itemsToHighlightGreen)
        {
            highlightManager.HighlightObject(item, Color.green);
        }

        // Iterate through the red-highlight array and call the highlight function
        foreach (GameObject item in itemsToHighlightRed)
        {
            highlightManager.HighlightObject(item, Color.red, 2f);
        }
    }

    public void DisableHighlights()
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
        audioSource.PlayOneShot(resolvedAudio);
        isFixed = true;
        laptopInteractable.active = true;
        CleanupHazard();
        Debug.Log("Hazard Resolved");
        DisableHighlights();
    }
}
