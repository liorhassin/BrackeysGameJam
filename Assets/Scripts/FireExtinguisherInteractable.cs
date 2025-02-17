using UnityEngine;

public class FireExtinguisherInteractable : Interactable
{
    /*public ParticleSystem extinguishEffect;
    public float extinguishDuration = 3f;
    private bool isUsing = false;*/

    public override void OnInteract()
    {

        FireHazard activeHazard = FindAnyObjectByType<FireHazard>();
        if (activeHazard != null && !activeHazard.isFixed)
        {
            activeHazard.ResolveHazard();
            Debug.Log("Fire Extinguished!");
            /*if (!isUsing)
            {
                StartCoroutine(UseExtinguisher());
            }*/
        }
        else
        {
            Debug.Log("Nothing to extinguish here.");
        }
    }

    /*private System.Collections.IEnumerator UseExtinguisher()
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
    }*/
}
