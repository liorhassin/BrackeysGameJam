using UnityEngine;

public class DoorInteractable : Interactable
{
    private bool isOpen = false;
    public float openRotation = 90f;
    public float closeRotation = 0f;
    public float doorSpeed = 3f;
    private bool isMoving = false;

    private Quaternion targetRotation;
    private Quaternion startRotation;
    private float transitionProgress = 0f;

    private void Start()
    {
        targetRotation = transform.rotation;
        startRotation = transform.rotation;
    }

    private void Update()
    {
        if (isMoving)
        {
            transitionProgress += Time.deltaTime * doorSpeed;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, transitionProgress);

            if (transitionProgress >= 1f)
            {
                isMoving = false;
                transitionProgress = 0f;
            }
        }
    }

    public override void OnInteract()
    {
        if (isMoving) return; 

        isOpen = !isOpen;
        startRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, isOpen ? transform.rotation.eulerAngles.y + 90f : transform.rotation.eulerAngles.y - 90f, transform.rotation.eulerAngles.z);
        isMoving = true;
        transitionProgress = 0f;

        Debug.Log(isOpen ? "Door Opened" : "Door Closed");
    }
}