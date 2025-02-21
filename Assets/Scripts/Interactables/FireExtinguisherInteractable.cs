using UnityEngine;

public class FireExtinguisherInteractable : Interactable
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
            UseExtinguisher();
        }
    }

    private void PickUp()
    {
        isCarried = true;
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("Picked up Fire Extinguisher.");
    }

    private void UseExtinguisher()
    {
        FireHazard activeHazard = FindAnyObjectByType<FireHazard>();
        if (activeHazard != null && !activeHazard.isFixed)
        {
            activeHazard.ResolveHazard();
            Debug.Log("Fire Extinguished!");
            Drop(); // Return extinguisher after use

            /* Uncomment if you want the particle effect to play when used
            if (!isUsing)
            {
                StartCoroutine(UseExtinguisherEffect());
            }
            */
        }
        else
        {
            Debug.Log("Nothing to extinguish here.");
        }
    }

    public void Drop()
    {
        isCarried = false;
        transform.SetParent(originalParent);
        transform.position = originalPosition;
        GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Fire Extinguisher Dropped.");
    }

    public bool IsCarried()
    {
        return isCarried;
    }

    /* Uncomment this function if you want a spray effect when using the extinguisher
    private System.Collections.IEnumerator UseExtinguisherEffect()
    {
        isUsing = true;
        if (extinguishEffect != null)
        {
            extinguishEffect.Play();
        }
        yield return new WaitForSeconds(extinguishDuration);
        if (extinguishEffect != null)
        {
            extinguishEffect.Stop();
        }
        isUsing = false;
    }
    */
}
