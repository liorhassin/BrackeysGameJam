using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    private float verticalInput;
    private float horizontalInput;
    private bool movementAllowed = true;

    [Header("References")]
    private CharacterController controller;
    public Transform orientation;
    public Camera playerCamera; // Reference to the player camera
    public float interactionRange = 3f; // How far the player can interact

    public PistolAnimations pistolAnimation;

    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float gravity = 9.81f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float decelerationRate = 10f; // Controls how fast the player slows down

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode interactKey = KeyCode.E;
    
    private float verticalVelocity;
    private Vector3 currentMoveDirection; // Stores movement for deceleration

    private float VerticalForceCalculation()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -1f;

            if (Input.GetKey(jumpKey))
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * gravity * 2);
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }
        return verticalVelocity;
    }

    private void InputManagement()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Movement()
    {
        GroundMovement();
    }

    private void Interact()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                interactable.OnInteract();
            }
        }
    }

    void Update()
    {
        if (movementAllowed)
        {
            InputManagement();
            Movement();
        }

        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }

        // Rotate the player to match orientation's rotation
        transform.rotation = Quaternion.Euler(0, orientation.eulerAngles.y, 0);
    }

    public void AllowMovement(bool b)
    {
        movementAllowed = b;
    }

    private void GroundMovement()
    {
        Vector3 targetMoveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        targetMoveDirection *= walkSpeed;

        // Apply deceleration when input stops
        if (horizontalInput == 0 && verticalInput == 0)
        {
            currentMoveDirection = Vector3.Lerp(currentMoveDirection, Vector3.zero, decelerationRate * Time.deltaTime);
        }
        else
        {
            currentMoveDirection = targetMoveDirection;
        }

        // Always apply gravity separately to prevent override
        verticalVelocity = VerticalForceCalculation();
        currentMoveDirection.y = verticalVelocity;

        controller.Move(currentMoveDirection * Time.deltaTime);
    }
}
