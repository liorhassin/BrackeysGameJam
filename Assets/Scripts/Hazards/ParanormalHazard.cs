using UnityEngine;

public class ParanormalHazard : Hazard
{
    public GameObject ghostEffect; 
    public AudioClip ghostSound;  

    private void Start()
    {
        hazardName = "Paranormal Activity";
        difficultyLevel = 10; 
        hazardDuration = 20;
        isFixed = true;

        if (ghostEffect != null)
        {
            ghostEffect.SetActive(false); 
        }
    }

    public override void TriggerHazard()
    {
        isFixed = false;
        Debug.Log("👻 Paranormal activity detected! The demons are here!");

        if (ghostEffect != null)
        {
            ghostEffect.SetActive(true); 
        }

        if (ghostSound != null && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(ghostSound);
        }
    }

    public override void CleanupHazard()
    {
        isFixed = true;
        Debug.Log("✝️ The demons have been banished!");

        if (ghostEffect != null)
        {
            ghostEffect.SetActive(false); 
        }
    }

    public override void ApplyFailure()
    {
        Debug.Log("😱 The demons consumed your soul! You lost the game.");
    }
}
