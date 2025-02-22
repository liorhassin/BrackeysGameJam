using UnityEngine;

public class FireExtinguisherInteractable : Interactable
{
    public ExtingItem handItem;

    private void Start()
    {
    }

    public override void OnInteract()
    {
        gameObject.SetActive(false);
        handItem.gameObject.SetActive(true);
    }
}
