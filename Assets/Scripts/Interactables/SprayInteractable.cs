using UnityEngine;

public class SprayInteractable : Interactable
{
    public SprayItem sprayItem;
    public override void OnInteract()
    {
        gameObject.SetActive(false);
        sprayItem.gameObject.SetActive(true);
    }
}
