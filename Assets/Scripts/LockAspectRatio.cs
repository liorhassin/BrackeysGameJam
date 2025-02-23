using UnityEngine;

public class LockAspectRatio : MonoBehaviour
{
    public float targetAspect = 16f / 9f; // Change this to your desired aspect ratio

    void Start()
    {
        UpdateAspectRatio();
    }

    void Update()
    {
        UpdateAspectRatio();
    }

    void UpdateAspectRatio()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = Camera.main;

        if (scaleHeight < 1.0f) // Add black bars at the top and bottom
        {
            Rect rect = camera.rect;
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
            camera.rect = rect;
        }
        else // Add black bars at the left and right
        {
            float scaleWidth = 1.0f / scaleHeight;
            Rect rect = camera.rect;
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
            camera.rect = rect;
        }
    }
}
