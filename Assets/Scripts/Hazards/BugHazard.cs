using TMPro;
using UnityEngine;

public class BugHazard : Hazard
{
    public Bug[] bugs;
    public SprayItem sprayItem;
    public SprayInteractable sprayInteractable;
    public GameObject desk;
    public TMP_Text tMP_Text;
    public Transform posTransform;

    private int currBugs;

    private void Start()
    {
        hazardName = "Bugs Attack!!";
        hazardDescription = "The house is full of bugs! Find the spray";
        tMP_Text.enabled = false;

        foreach (Bug b in bugs){
            b.gameObject.SetActive(false);
            highlightManager.HighlightObject(b.gameObject, Color.red, 2f);
        }

        sprayInteractable.active = false;
        sprayItem.gameObject.SetActive(false);
    }

    public override void TriggerHazard()
    {
        isFixed = false;

        sprayInteractable.gameObject.SetActive(true);
        sprayInteractable.active = true;

        foreach (Bug b in bugs)
        {
            b.gameObject.SetActive(true);
        }

        currBugs = bugs.Length;
    }

    public override void CleanupHazard()
    {
        Debug.Log("The fire has been extinguished.");
        foreach (Bug b in bugs)
        {
            b.gameObject.SetActive(false);
        }

        desk.transform.position = posTransform.transform.position;
        tMP_Text.enabled = true;
        sprayItem.gameObject.SetActive(false);
    }

    public override void ApplyFailure()
    {
        Debug.Log("The fire spread! You lost the game.");
    }

    public void onBugDead()
    {
        currBugs--;
        if (currBugs <= 0)
        {
            ResolveHazard();
        }
    }
}
