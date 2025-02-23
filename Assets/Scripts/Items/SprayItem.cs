using UnityEngine;

public class SprayItem : HandItem
{
    public ParticleSystem whiteSmoke;
    public Camera fpsCam;
    public RaycastHit rayHit;
    public float range;
    public LayerMask layerMask;
    public AudioSource audioSource;
    

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
                whiteSmoke.Play();
            }
            if (!audioSource.isPlaying){
                audioSource.Play();
            }
            if (rayHit.collider.CompareTag("Bug"))
            {
                Bug f = rayHit.collider.GetComponent<Bug>();
                if (f != null)
                {
                    f.AddTime(Time.deltaTime);
                }
            }
        }
        else{
            whiteSmoke.Stop();
        }
    }

    public override void StopUsing()
    {
        Invoke(nameof(StopParticles), 1f);
        audioSource.Stop();
    }

    private void StopParticles()
    {
        whiteSmoke.Stop();

    }
}
