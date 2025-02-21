using UnityEngine;

[RequireComponent(typeof(Renderer))]
public abstract class Interactable : MonoBehaviour
{
    private Outline outline;

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
    }

    // Enable the outline when the player looks at the object
    public void EnableOutline()
    {
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    // Disable the outline when the player stops looking at the object
    public void DisableOutline()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }

    public abstract void OnInteract();
}
