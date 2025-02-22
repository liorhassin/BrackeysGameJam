using UnityEngine;
using UnityEngine.AI; // Required for NavMesh navigation
using System.Collections;

public class DemonControl : MonoBehaviour
{

    private NavMeshAgent navMeshAgent; // Pathfinding
    public Transform player;
    public float interactionRange = 3f;
    public float destroyTime;
    public int damage = int.MaxValue;
    public AudioSource audioSource;
    public CrossItem crossItem;
    public ParticleSystem fire;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.5f;

    public bool attacked = false;

    public enum State
    {
        Walk,
        Attack,
        Dead
    }

    public State currentState = State.Walk; // Default state is Walk

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.speed = walkSpeed;
            navMeshAgent.angularSpeed = 240f; // Make the zombie rotate smoothly towards the player
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();

        if (!audioSource.isPlaying){
            audioSource.Play();
        }

        switch (currentState)
        {
            case State.Walk:
                navMeshAgent.destination = player.position;
                navMeshAgent.speed = walkSpeed;
                break;
            case State.Attack:
                navMeshAgent.speed = 0f;
                if (!attacked){
                    attacked = true;
                    AttackPlayer();
                }
                break;
            case State.Dead:
                break;
        }
    }

    private void DetectPlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactionRange && currentState != State.Dead)
        {
            currentState = State.Attack; // Switch to attack state if in range
        }
        else if (distance > interactionRange && currentState != State.Dead)
        {
            currentState = State.Walk; // Switch to walk state if not attacking
        }
    }

    private void AttackPlayer()
    {
        if (crossItem.gameObject.activeSelf){
            Die();
        }
        else{
            HealthSystem hpSystem = player.GetComponent<HealthSystem>();
            if (hpSystem != null){
                hpSystem.Damage(damage);
            }
        }
    }

    public void Die() 
    {
        navMeshAgent.speed = 0f;

        Debug.Log("play fire");
        fire.Play();
        
        StartCoroutine(DeactivateAfterDeath(destroyTime)); // Start countdown to destroy zombie after death
    }

    private IEnumerator DeactivateAfterDeath(float time)
    {
        yield return new WaitForSeconds(time);

        currentState = State.Dead;
    }
}
