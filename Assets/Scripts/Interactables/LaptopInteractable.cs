using UnityEngine;

public class LaptopInteractable : Interactable
{
    public Transform laptopCameraPosition;
    public Camera playerCamera;
    public PlayerController playerMovement;
    public PlayerCamera playerCameraScript;
    public GameObject playerUIDot;
    public TypingManager typingManager;
    private bool isUsingLaptop = false;
    private Quaternion originalCameraRotation;
    private Vector3 originalCameraPosition;
    public float transitionSpeed = 3f;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerController>();
        if (playerCameraScript == null)
            playerCameraScript = FindFirstObjectByType<PlayerCamera>();
    }

    public override void OnInteract()
    {
        if (!isUsingLaptop)
        {
            EnterLaptopMode();
        }
    }

    private void EnterLaptopMode()
    {
        isUsingLaptop = true;
        originalCameraPosition = playerCamera.transform.position;
        originalCameraRotation = playerCamera.transform.rotation;
        playerMovement.AllowMovement(false);
        playerCameraScript.enable_camera(false);
        playerUIDot.SetActive(false);
        typingManager.enableTyping();
    }

    private void Update()
    {
        if (isUsingLaptop)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, laptopCameraPosition.position, Time.deltaTime * transitionSpeed);
            playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, laptopCameraPosition.rotation, Time.deltaTime * transitionSpeed);
        }

        if (isUsingLaptop && Input.GetMouseButtonDown(0))
        {
            ExitLaptopMode();
        }
    }

    private void ExitLaptopMode()
    {
        isUsingLaptop = false;
        StartCoroutine(SmoothReturnToPlayer());
        playerCameraScript.enable_camera(true);
        playerUIDot.SetActive(true);
        typingManager.disableTyping();
    }

    private System.Collections.IEnumerator SmoothReturnToPlayer()
    {
        while (Vector3.Distance(playerCamera.transform.position, originalCameraPosition) > 0.01f)
        {
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, originalCameraPosition, Time.deltaTime * transitionSpeed);
            playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, originalCameraRotation, Time.deltaTime * transitionSpeed);
            yield return null;
        }
        playerMovement.AllowMovement(true);
    }
}
