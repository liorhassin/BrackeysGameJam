using UnityEngine;

public class TestInteractable : Interactable
{
    public override void OnInteract()
    {
        Debug.Log("Interacted with TestInteractable");
    }
}
