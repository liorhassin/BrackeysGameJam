using UnityEngine;
using UnityEngine.AI; // Required for NavMesh navigation
using System.Collections;
using UnityEngine.UI;

public class ZombieControl : MonoBehaviour
{
    [Header("References")]
    private NavMeshAgent navMeshAgent; // Pathfinding
    private Animation animator; // Animator for handling animations
    private HealthSystem hpSys;
    public Transform player;
    public float interactionRange = 3f;
    public float destroyTime = 20f;
    public AudioSource audioSource;
    public AudioClip[] zombieSounds;

    private int damage = 1;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.5f;

    private ZombieAttack zombieAttackScript;
    
    //Testing new damage function:
    private GameObject healthBarGameObject;
    private Slider playerHealthSlider;
    

    // Enum to represent zombie states
    public enum ZombieState
    {
        Walk,
        Attack,
        Dead
    }

    public ZombieState currentState = ZombieState.Walk; // Default state is Walk

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animation>(); // Get the Animator component
        hpSys =  GetComponent<HealthSystem>();
        zombieAttackScript = GetComponentInParent<ZombieAttack>();
        animator["Walk"].speed = 2f;

        if (navMeshAgent != null)
        {
            navMeshAgent.speed = walkSpeed;
            navMeshAgent.angularSpeed = 240f; // Make the zombie rotate smoothly towards the player
        }

        healthBarGameObject = GameObject.Find("UICanvas/HUD/HealthBar");
        playerHealthSlider = healthBarGameObject.GetComponent<Slider>();

        StartCoroutine(PlayRandomSound());
    }

    void Update()
    {
        // Update the state of the zombie based on the player's position
        DetectPlayer();

        if (hpSys.isDead && currentState != ZombieState.Dead){
            animator.Play("Death1"); // Play death animation
            navMeshAgent.speed = 0f;
            Die();
        }

        // Apply the state machine
        switch (currentState)
        {
            case ZombieState.Walk:
                Vector3 closestPoint;
                if (FindClosestPointOnNavMesh(player.position, out closestPoint))
                {
                    
                }
                navMeshAgent.destination = closestPoint;
                navMeshAgent.speed = walkSpeed;
                break;
            case ZombieState.Attack:
                if (!animator.IsPlaying("Attack1")) // Check if Attack1 animation has finished
                {
                    navMeshAgent.speed = 0f;
                    animator.Play("Attack1");
                    AttackPlayer();
                }
                break;
            case ZombieState.Dead:
                break;
        }
    }

    private void DetectPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange && currentState != ZombieState.Dead)
        {
            currentState = ZombieState.Attack; 
        }
        else if (distance > interactionRange && currentState != ZombieState.Dead)
        {
            currentState = ZombieState.Walk; // Switch to walk state if not attacking
            animator.Play("Walk"); // Play walk animation
        }
    }

    private void AttackPlayer()
    {
        Debug.Log("attackkk");
        HealthSystem hpSystem = player.GetComponent<HealthSystem>();
        if (hpSystem != null){
            hpSystem.DamageAndUpdateUI(damage, playerHealthSlider);
        }
    }

    public void Die() 
    {
        currentState = ZombieState.Dead; // Switch to dead state
        
        // Disable the collider to avoid the player getting stuck
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // Disable the collider
        }

        // Optionally, you can also disable the ZombieControl script if needed
        this.enabled = false; // Disables the entire script
        
        if (zombieAttackScript != null)
        {
            zombieAttackScript.OnZombieDied(gameObject);
        }
        
        StartCoroutine(DestroyAfterDeath(destroyTime)); // Start countdown to destroy zombie after death
    }



    private IEnumerator DestroyAfterDeath(float time)
    {
        // Wait for the specified time after death (20 seconds)
        yield return new WaitForSeconds(time);

        // Destroy the zombie
        Destroy(gameObject);
    }

    IEnumerator PlayRandomSound()
    {
        while (true && currentState != ZombieState.Dead)
        {
            yield return new WaitForSeconds(Random.Range(5f, 10f)); // Wait 5-10 sec

            if (zombieSounds.Length > 0 && currentState != ZombieState.Dead)
            {
                AudioClip clip = zombieSounds[Random.Range(0, zombieSounds.Length)]; // Pick random sound
                audioSource.PlayOneShot(clip); // Play sound
            }
        }
    }

    bool FindClosestPointOnNavMesh(Vector3 position, out Vector3 closestPoint)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, 10f, NavMesh.AllAreas))
        {
            closestPoint = hit.position;
            return true;
        }

        closestPoint = Vector3.zero;
        return false;
    }
}
