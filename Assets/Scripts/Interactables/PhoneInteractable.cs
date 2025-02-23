using System.Collections;
using UnityEngine;

public class PhoneInteractable : Interactable
{
    public AudioSource audioSource;
    public PhoneHazard phoneHazard;

    public override void OnInteract()
    {
        PhoneHazard activeHazard = FindAnyObjectByType<PhoneHazard>();

        if (activeHazard != null && !activeHazard.isFixed)
        {
            activeHazard.PhoneAnswered();
        }
        else
        {
            Debug.Log("Nothing to fix here.");
        }
    }
}
