using UnityEngine;

public class PhoneHazard : Hazard
{
    public AudioClip phoneRingSound;

    private void Start()
    {
        hazardName = "Phone Ringing";
        difficultyLevel = 0; 
        hazardDuration = 15;
        isFixed = true;
    }

    public override void TriggerHazard()
    {
        isFixed = false;
        Debug.Log("☎️ The phone is ringing! Answer it before it's too late.");

        if (phoneRingSound != null && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(phoneRingSound);
        }
    }

    public override void CleanupHazard()
    {
        isFixed = true;
        Debug.Log("✅ Phone call ended.");
    }

    public override void ApplyFailure()
    {
        Debug.Log("🚨 You ignored the call! Something bad happened.");
    }
}
