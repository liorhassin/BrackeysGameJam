using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class Interactable : MonoBehaviour
{
    private Outline outline;
    public AudioSource interactAudio;
    public bool active = true;
    public bool alwaysOutline = false;

    private void Awake()
    {
        // Add an Outline component if it doesn't already exist
        outline = gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }

        // Set default outline properties
        outline.OutlineMode = Outline.Mode.OutlineAll;
        outline.OutlineColor = Color.white;
        outline.OutlineWidth = 5f;

        // Disable the outline initially
        outline.enabled = false;

        if (interactAudio != null){
            interactAudio.loop = false;
            interactAudio.playOnAwake = false;
        }
    }

    // Enable the outline when the player looks at the object
    public void EnableOutline()
    {
        if (!active){
            return;
        }
        
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    // Disable the outline when the player stops looking at the object
    public void DisableOutline()
    {
        if (outline != null && !alwaysOutline)
        {
            outline.enabled = false;
        }
    }

    public void Interact(){
        if (active){
            if (interactAudio != null){
                interactAudio.Play();
            }
            OnInteract();
        }
    }

    public void setColor(Color color){
        outline.OutlineColor = color;
    }

    public abstract void OnInteract();
}
