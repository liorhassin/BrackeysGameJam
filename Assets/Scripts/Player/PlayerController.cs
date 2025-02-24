using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    private float verticalInput;
    private float horizontalInput;
    private bool movementAllowed = true;
    float xRotation;
    float yRotation;
    public static float mouseSensitivityX = 500f;
    public static float mouseSensitivityY = 500f;
    public static float sensitivityMuliplier = 0.5f;
    
    public Transform camerTransform;

    [Header("References")]
    private CharacterController controller;
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

    [Header("Interactable")]
    private Interactable currentInteractable;
    
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
        mouseSensitivityX  *= sensitivityMuliplier;
        mouseSensitivityY *= sensitivityMuliplier;
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
                interactable.Interact();
            }
        }
    }

    private void Look(){
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
        camerTransform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void Update()
    {
        DetectInteractable();

        if (movementAllowed)
        {
            InputManagement();
            Movement();
            Look();
        }

        if (Input.GetKeyDown(interactKey))
        {
            Interact();
        }
    }

    public void AllowMovement(bool b)
    {
        movementAllowed = b;
    }

    private void GroundMovement()
    {
        Vector3 targetMoveDirection = camerTransform.forward * verticalInput + camerTransform.right * horizontalInput;
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

    private void DetectInteractable()
    {
        // Perform a raycast from the camera
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null)
            {
                // If the player is looking at a new interactable, disable the old one first
                if (currentInteractable != interactable)
                {
                    DisableCurrentInteractable();
                    currentInteractable = interactable;
                    currentInteractable.EnableOutline();
                }
            }
            else
            {
                // No interactable detected, disable the outline
                DisableCurrentInteractable();
            }
        }
        else
        {
            // If nothing is hit, disable any active outline
            DisableCurrentInteractable();
        }
    }

    private void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }

    public static void SetSensitivity(float sen)
    {
        sensitivityMuliplier = sen;
        mouseSensitivityX = sen * 500f;
        mouseSensitivityY = sen * 500f;
    }
}
