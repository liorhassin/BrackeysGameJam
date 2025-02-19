using UnityEngine;

public class Demon : Interactable
{
    public ParanormalHazard hazard;

    public override void OnInteract()
    {
        CrossInteractable cross = FindAnyObjectByType<CrossInteractable>();

        if (cross != null && cross.IsCarried())
        {
            CleanseDemon();
            cross.Drop(); // Drop the cross after use
        }
        else
        {
            Debug.Log("😈 You need to be holding a Cross to cleanse the demon!");
        }
    }

    public void CleanseDemon()
    {
        if (hazard != null)
        {
            hazard.CleanupHazard(); // Cleanses all paranormal activity
        }

        gameObject.SetActive(false); // The demon disappears
        Debug.Log("👻 The demon has been cleansed!");
    }
}
