using UnityEngine;

public class PhoneHazard : Hazard
{
    public AudioSource phoneRingSound;
    public Interactable phone;

    private string f1 = "phone_start";
    private string f2 = "A hazard has appeared! Use the glowing outline to find it, even through walls.";
    private string f3 = "phone_fixed";
    private string f4 = "You fixed the first hazard! Go back to your laptop to continue working on your assignment.";

    private void Start()
    {
        hazardName = "Mom is calling";
        hazardDescription = "It's mom, you got to answer";
        difficultyLevel = 0; 
        isFixed = false;
        phone.active = false;
    }

    public override void TriggerHazard()
    {
        TutorialManager.instance.ShowTutorial(f1, f2);

        isFixed = false;
        Debug.Log("☎️ The phone is ringing! Answer it before it's too late.");

        phone.active = true;

        if (phoneRingSound != null)
        {
            Debug.Log("play phone ring");
            phoneRingSound.Play();
        }
    }

    public void PhoneAnswered(){
        phoneRingSound.Stop();
        phone.active = false;

        Invoke("Halas", 5f);
    }

    public void Halas(){
        ResolveHazard();
    }

    public override void CleanupHazard()
    {
        TutorialManager.instance.ShowTutorial(f3, f4);
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
