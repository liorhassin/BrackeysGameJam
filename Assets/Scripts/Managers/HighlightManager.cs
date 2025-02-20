using UnityEngine;
using System.Collections.Generic;

public class HighlightManager : MonoBehaviour
{
    public static HighlightManager Instance;

    private Dictionary<GameObject, Outline> outlines = new Dictionary<GameObject, Outline>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private Outline GetOrCreateOutline(GameObject obj, Color color)
    {
        if (!outlines.ContainsKey(obj))
        {
            Outline outline = obj.GetComponent<Outline>();
            if (outline == null)
            {
                outline = obj.AddComponent<Outline>(); // Add Quick Outline if missing
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineWidth = 5f; // Adjust width
            }
            outline.OutlineColor = color;
            outlines[obj] = outline;
        }
        return outlines[obj];
    }

    public void HighlightObjectWhite(GameObject obj)
    {
        Outline outline = GetOrCreateOutline(obj, Color.white);
        outline.enabled = true;
    }

    public void HighlightObjectRed(GameObject obj)
    {
        Outline outline = GetOrCreateOutline(obj, Color.red);
        outline.enabled = true;
    }

    public void DisableHighlight(GameObject obj)
    {
        if (outlines.ContainsKey(obj))
        {
            outlines[obj].enabled = false;
        }
    }
}
