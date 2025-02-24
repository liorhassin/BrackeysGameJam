using System;
using System.Xml.Serialization;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private bool camera_enabled = true;

    public Transform targetTransform;
    public PlayerController playerMovement;
    private float transitionSpeed = 5f;
    private float minSpeed = 0.05f;

    public enum State {
        OnTarget,
        Tarnsition,
    }

    private State currState;
    private bool allowMovementTransition = true;

    public void enable_camera(bool b)
    {
        camera_enabled = b;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
        currState = State.OnTarget;
    }

    public void ChangeCameraTarget(Transform target, bool allowMovement){
        targetTransform = target;
        currState = State.Tarnsition;
        allowMovementTransition = allowMovement;
        if (!allowMovement){
            playerMovement.AllowMovement(allowMovement);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (currState == State.Tarnsition)
        {
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            
            // Adjusted speed to prevent extreme slowdowns
            float dynamicSpeed = Mathf.Max(minSpeed, transitionSpeed * Time.deltaTime); // Ensures a minimum speed

            transform.position = Vector3.Lerp(transform.position, targetTransform.position,  dynamicSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, dynamicSpeed);


            if (distance < 0.007f)
            {
                currState = State.OnTarget;
                playerMovement.AllowMovement(allowMovementTransition);
            }
        }

        if (currState == State.OnTarget)
        {
            transform.position = targetTransform.position;
            transform.rotation = targetTransform.rotation;
            return;
        }
    }

}
