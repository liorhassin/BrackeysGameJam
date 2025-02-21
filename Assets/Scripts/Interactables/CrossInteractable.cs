using UnityEngine;

public class CrossInteractable : Interactable
{
    public Transform playerHand; // Assign this in Unity Inspector (empty GameObject in front of the player)
    private bool isCarried = false;
    private Transform originalParent;
    private Vector3 originalPosition;

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
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("Picked up the Holy Cross.");
    }

    public void UseCross()
    {
        Demon activeDemon = FindAnyObjectByType<Demon>();
        if (activeDemon != null)
        {
            activeDemon.CleanseDemon();
            Debug.Log("✝️ You used the cross! The demon has been cleansed.");
            Drop(); // Return the cross after use
        }
        else
        {
            Debug.Log("There is no demon to cleanse.");
        }
    }

    public void Drop()
    {
        isCarried = false;
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Cross Dropped.");
    }

    public bool IsCarried()
    {
        return isCarried;
    }
}
