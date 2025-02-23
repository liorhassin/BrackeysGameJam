using UnityEngine;

public class Bug : MonoBehaviour
{
    public BugHazard bugHazard;
    
    public float timeToExtinguish = 1f;
    private float timeGot = 0f;

    public void AddTime(float t)
    {
        timeGot += t;
        if (timeGot >= timeToExtinguish)
        {
            StopFire();
        }
    }

    public void StopFire()
    {
        bugHazard.onBugDead();
        gameObject.SetActive(false);
    }
}
