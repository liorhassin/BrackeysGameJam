using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    [Header("Gun Stats")]
    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
    public AudioSource shotSound;
    public AudioClip shotClip;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    private GameObject muzzleInstance; // Reference to the muzzle flash instance
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;
    public PistolAnimations pistolAnimation;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;

        // Instantiate the muzzle flash once and disable it initially
        muzzleInstance = Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        muzzleInstance.SetActive(false);
        muzzleInstance.layer = LayerMask.NameToLayer("Pistol");
    }

    private void Update()
    {
        MyInput();

        // SetText
        // text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        // Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        if (pistolAnimation != null)
        {
            pistolAnimation.Shoot();
        }

        if (shotSound != null && shotClip != null){
            shotSound.PlayOneShot(shotClip);
        }

        // Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        // RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);
            // Instantiate the bullet hole at the hit point
            GameObject bulletHole = Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            // If the hit object is movable (has a Rigidbody), make the bullet hole a child of that object
            bulletHole.transform.SetParent(rayHit.collider.transform);

            // If we hit an enemy, deal damage
            if (rayHit.collider.CompareTag("Enemy"))
            {
                HealthSystem hpSystem = rayHit.collider.GetComponent<HealthSystem>();
                if (hpSystem != null)
                {
                    hpSystem.Damage(damage);
                }
                
                Destroy(bulletHole, 0.2f); // Destroy after 0.2 seconds
            }
        }

        // Reuse the muzzle flash instead of instantiating a new one
        muzzleInstance.SetActive(true);
        muzzleInstance.transform.position = attackPoint.position;
        muzzleInstance.transform.rotation = attackPoint.rotation;

        Invoke("ResetMuzzleFlash", 0.1f);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    // Resets the muzzle flash after it's been shown for a brief moment
    private void ResetMuzzleFlash()
    {
        muzzleInstance.SetActive(false);  // Deactivate the muzzle flash
    }
}
