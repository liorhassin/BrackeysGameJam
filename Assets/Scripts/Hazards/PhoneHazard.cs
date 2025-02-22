using UnityEngine;

public class PhoneHazard : Hazard
{
    public AudioSource phoneRingSound;
    public Interactable phone;

    private void Start()
    {
        hazardName = "Phone Ringing";
        difficultyLevel = 0; 
        isFixed = true;
        phone.active = false;
    }

    public override void TriggerHazard()
    {
        isFixed = false;
        Debug.Log("☎️ The phone is ringing! Answer it before it's too late.");

        phone.active = true;

        if (phoneRingSound != null)
        {
            Debug.Log("play phone ring");
            phoneRingSound.Play();
        }
    }

    public override void CleanupHazard()
    {
        isFixed = true;
        phoneRingSound.Stop();
        phone.active = false;
        Debug.Log("✅ Phone call ended.");
    }

    public override void ApplyFailure()
    {
        Debug.Log("🚨 You ignored the call! Something bad happened.");
    }
}
