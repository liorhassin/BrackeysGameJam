using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    float xRotation;
    float yRotation;
    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;
    public Transform orientation;

    private bool camera_enabled = true;

    public void enable_camera(bool b){
        camera_enabled = b;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camera_enabled){
            return;
        }
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
}
