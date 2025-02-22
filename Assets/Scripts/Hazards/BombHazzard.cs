using UnityEngine;
using TMPro;

public class BombHazzard : Hazard
{
    public Timer timer;
    public TMP_Text text;
    public HealthSystem playerHp;
    public GameObject bomb;
    public DiffuseManager diffuseManager;

    public AudioSource beepSound;
    public AudioSource boomSound;

    private bool triggered = false;

    void Start()
    {
        bomb.SetActive(false);
        isFixed = false;
        timer.time = hazardDuration;
    }

    void Update()
    {
        if (diffuseManager.isSolved)
        {
            isFixed = true;
            timer.StopTimer();
            ResolveHazard();
        }

        if (triggered)
        {
            // Update timer and display time left
            text.text = Mathf.CeilToInt(timer.TimeLeft()).ToString();

            // If time runs out, trigger failure
            if (timer.isFinished)
            {
                Debug.Log("is finished");
                ApplyFailure();
            }
        }
    }

    public override void ApplyFailure()
    {
        Debug.Log("kill player");
        playerHp.Damage(int.MaxValue); // Instant failure (player dies)
        boomSound.Play();
        CleanupHazard();
    }

    public override void CleanupHazard()
    {
        DisableHighlights();
        triggered = false;
        beepSound.Stop();
    }

    public override void TriggerHazard()
    {
        bomb.SetActive(true);
        timer.StartTimer();
        triggered = true;
        isFixed = false;
        diffuseManager.isSolved = false;
        beepSound.Play();
    }
}
