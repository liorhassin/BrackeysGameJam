using UnityEngine;

public class PhoneHazard : Hazard
{
    public AudioSource phoneRingSound;
    public Interactable phone;

    private void Start()
    {
        hazardName = "Phone Ringing";
        hazardDescription = "Answer the phone before the caller hangs up.";
        difficultyLevel = 0; 
        isFixed = true;
        phone.active = false;
    }

    public override void TriggerHazard()
    {
        TutorialManager.instance.ShowTutorial("phone_start", "A hazard has appeared! Use the glowing outline to find it, even through walls.");

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
        TutorialManager.instance.ShowTutorial("phone_fixed", "You fixed the first hazard! Go back to your laptop to continue working on your assignment.");
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
