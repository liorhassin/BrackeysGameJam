using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float timeToExtinguish = 3f;
    public float timeGot = 0f;
    public FireHazard fireHazard;
    public List<ParticleSystem> particleSystems;
    

    public void Activate()
    {
        Debug.Log("fire activated");
        gameObject.SetActive(true);
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Play();
        }
    }

    public void Deactivate()
    {
        foreach (ParticleSystem particleSystem in particleSystems)
        {
            particleSystem.Stop();
        }
        gameObject.SetActive(false);
    }

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
        fireHazard.onFireDead();
        gameObject.SetActive(false);
    }
}
