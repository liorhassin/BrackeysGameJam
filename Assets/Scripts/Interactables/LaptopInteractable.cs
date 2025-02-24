using UnityEngine;
using System.Collections;

public class LaptopInteractable : Interactable
{
    public Transform laptopCameraPosition;
    public Transform playerCameraTransform;
    public Camera playerCamera;
    public PlayerController playerMovement;
    public PlayerCamera playerCameraScript;
    public GameObject playerUIDot;
    public TypingManager typingManager;
    private bool isUsingLaptop = false;
    public float transitionSpeed = 3f;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
        if (playerMovement == null)
            playerMovement = FindFirstObjectByType<PlayerController>();
        if (playerCameraScript == null)
            playerCameraScript = FindFirstObjectByType<PlayerCamera>();
        setColor(Color.yellow);
        EnableOutline();

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
        TutorialManager.instance.ShowTutorial("laptop", "Press any key to start typing. \nPress Left Mouse Button to exit the laptop anytime.");
        isUsingLaptop = true;
        playerUIDot.SetActive(false);

        playerCameraScript.ChangeCameraTarget(laptopCameraPosition, false);
        StartCoroutine(EnableTypingWithDelay(0.2f));
    }

    private IEnumerator EnableTypingWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (typingManager != null && isUsingLaptop)
        {
            typingManager.EnableTyping();
        }
    }

    private void Update()
    {
        if (isUsingLaptop && Input.GetMouseButtonDown(0))
        {
            ExitLaptopMode();
        }
    }

    public void ExitLaptopMode()
    {
        isUsingLaptop = false;
        playerCameraScript.ChangeCameraTarget(playerCameraTransform, true);
        playerUIDot.SetActive(true);
        if (typingManager != null)
        {
            typingManager.DisableTyping();
        }
    }
}
