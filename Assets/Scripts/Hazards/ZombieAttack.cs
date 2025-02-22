using UnityEngine;
using System.Collections;

public class ZombieAttack : Hazard
{
    public GameObject zombiePrefab;  // The zombie prefab to spawn
    public Transform spawnPointsParent;  // Reference to the SpawnPoints GameObject
    public int numberOfZombies = 5;  // Number of zombies to spawn
    public float spawnInterval = 1f; // Interval in seconds between spawns
    public GameObject pistol;
    public Transform player;  // Player transform to assign to each zombie

    private Transform[] spawnPoints;  // Array to store spawn points
    private int zombiesAlive;  // Counter to track how many zombies are alive

    private void Start()
    {
        hazardName = "Zombie Attack!";
        hazardDuration = 3000;
        isFixed = true;

        // Populate the spawnPoints array with all child transforms under spawnPointsParent
        if (spawnPointsParent != null)
        {
            spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>();

            // Remove the parent transform from the list (it is also in the array)
            spawnPoints = System.Array.FindAll(spawnPoints, t => t != spawnPointsParent);
        }
    }

    public override void TriggerHazard()
    {
        isFixed = false;
        Debug.Log("Zombies Are Attacking!!! Kill Them All");
        pistol.SetActive(true);

        // Start spawning zombies with a delay
        StartCoroutine(SpawnZombiesOverTime());
    }

    public override void CleanupHazard()
    {
        player.GetComponent<HealthSystem>().Heal(100);
        pistol.SetActive(false);
    }

    public override void ApplyFailure()
    {
        Debug.Log("You Are Dead");
    }

    private IEnumerator SpawnZombiesOverTime()
    {
        int zombiesSpawned = 0;

        while (zombiesSpawned < numberOfZombies)
        {
            // Randomly choose a spawn point from the array
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the zombie at the chosen spawn point as a child of this GameObject
            GameObject spawnedZombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation, transform);

            // Ensure the zombie gets the player reference
            ZombieControl zombieScript = spawnedZombie.GetComponent<ZombieControl>();
            if (zombieScript != null)
            {
                zombieScript.player = player;  // Set the player reference for the zombie
            }
            highlightManager.HighlightObject(spawnedZombie, Color.red, 2f);

            zombiesSpawned++;
            zombiesAlive++;

            // Wait for the specified spawn interval before spawning the next zombie
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Update(){
        if (zombiesAlive > 0 && !pistol.activeInHierarchy){
            pistol.SetActive(true);
        }
    }

    // This method is called when a zombie dies
    public void OnZombieDied(GameObject zombie)
    {
        zombiesAlive--;
        highlightManager.DisableHighlight(zombie);
        // Check if all zombies are dead
        if (zombiesAlive <= 0)
        {
            Debug.Log("All zombies are dead!");
            ResolveHazard();
        }
    }
}
