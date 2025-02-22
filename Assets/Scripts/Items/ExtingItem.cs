using System.Collections.Generic;
using UnityEngine;

public class ExtingItem : HandItem
{

    public ParticleSystem whiteSmoke;
    public Camera fpsCam;
    public RaycastHit rayHit;
    public float range;
    public LayerMask layerMask;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void Use()
    {
        Vector3 direction = fpsCam.transform.forward;


        // RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, layerMask))
        {
            whiteSmoke.transform.position = rayHit.point;
            whiteSmoke.transform.rotation = Quaternion.LookRotation(rayHit.normal);
            if (!whiteSmoke.isPlaying){
                Debug.Log("smokeeee");
                whiteSmoke.Play();
            }
            if (rayHit.collider.CompareTag("Fire"))
            {
                Debug.Log("found fire");
                Fire f = rayHit.collider.GetComponent<Fire>();
                if (f != null)
                {
                    f.AddTime(Time.deltaTime);
                }
            }
            else{
                Debug.Log("no fire");
            }
        }
        else{
            whiteSmoke.Stop();
        }
    }

    public override void StopUsing()
    {
        Invoke(nameof(StopParticles), 1f);
    }

    private void StopParticles()
    {
        whiteSmoke.Stop();
    }

}
