using UnityEngine;

public class CrossInteractable : Interactable
{
    private bool isCarried = false;
    private Transform originalParent;
    private Vector3 originalPosition;
    public CrossItem crossItem;

    private void Start()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
    }

    public override void OnInteract()
    {
        if (!isCarried)
        {
            PickUp();
        }
        else
        {
            Debug.Log("You are already holding the cross!");
        }
    }

    private void PickUp()
    {
        isCarried = true;
        gameObject.SetActive(false);
        crossItem.gameObject.SetActive(true);

        Debug.Log("Picked up the Holy Cross.");
    }

    public bool IsCarried()
    {
        return isCarried;
    }
}
