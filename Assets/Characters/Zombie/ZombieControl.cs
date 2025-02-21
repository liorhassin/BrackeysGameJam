using UnityEngine;
using UnityEngine.AI; // Required for NavMesh navigation
using System.Collections;

public class ZombieControl : MonoBehaviour
{
    [Header("References")]
    private NavMeshAgent navMeshAgent; // Pathfinding
    private Animation animator; // Animator for handling animations
    private HealthSystem hpSys;
    public Transform player;
    public float interactionRange = 3f;
    public float destroyTime = 20f;

    private int damage = 1;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.5f;
    private float verticalVelocity;

    private ZombieAttack zombieAttackScript;

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
                navMeshAgent.destination = player.position;
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
            currentState = ZombieState.Attack; // Switch to attack state if in range
            Debug.Log("Player detected! Preparing to attack.");
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
            hpSystem.Damage(damage);
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

    // You can call the Die method from other parts of the game when the zombie dies
}
