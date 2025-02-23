using UnityEngine;

public class ParanormalHazard : Hazard
{
    public DemonControl ghostEffect; 
    public AudioClip ghostSound;  
    public CrossItem crossItem;
    public CrossInteractable crossInteractable;
    public Transform startPos;

    private void Start()
    {
        hazardName = "Paranormal Activity";
        hazardDescription = "Hold the cross tight before the demon reaches you!";
        isFixed = true;
        crossInteractable.active = false;

        if (ghostEffect.gameObject != null)
        {
            ghostEffect.gameObject.SetActive(false); 
        }
    }

    public override void TriggerHazard()
    {
        isFixed = false;
        Debug.Log("👻 Paranormal activity detected! The demons are here!");

        crossInteractable.active = true;
        crossInteractable.gameObject.SetActive(true);

        if (ghostEffect != null)
        {
            ghostEffect.gameObject.transform.position = startPos.transform.position;
            ghostEffect.Reset();
            ghostEffect.gameObject.SetActive(true); 
            ghostEffect.fire.Stop();
        }

        if (ghostSound != null && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(ghostSound);
        }
    }

    public void Update()
    {
        if (ghostEffect.currentState == DemonControl.State.Dead){
            ResolveHazard();
        }
    }

    public override void CleanupHazard()
    {
        Debug.Log("✝️ The demons have been banished!");

        if (ghostEffect.gameObject != null)
        {
            ghostEffect.fire.Stop();
            ghostEffect.gameObject.SetActive(false); 
        }

        if (crossItem.gameObject != null){
            crossItem.gameObject.SetActive(false);
        }

        crossInteractable.active = false;
    }

    public override void ApplyFailure()
    {
        Debug.Log("😱 The demons consumed your soul! You lost the game.");
        if (crossItem != null){
            crossItem.gameObject.SetActive(false);
        }
    }
}
