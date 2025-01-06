using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    // Set your design aspect ratio (e.g., 16:9)
    public float baseAspectRatio = 16f / 9f;
    public float baseOrthographicSize = 5f; // Default orthographic size

    void Start()
    {
        AdjustCameraSize();
    }

    void AdjustCameraSize()
    {
        // Get the current screen's aspect ratio
        float currentAspectRatio = (float)Screen.width / Screen.height;

        // Calculate the new orthographic size based on the aspect ratio
        float orthographicSize = baseOrthographicSize;

        if (currentAspectRatio > baseAspectRatio)
        {
            // If the screen is wider than the base aspect ratio, adjust vertically
            orthographicSize = baseOrthographicSize * (currentAspectRatio / baseAspectRatio);
        }
        else
        {
            // If the screen is taller than the base aspect ratio, adjust horizontally
            orthographicSize = baseOrthographicSize / (baseAspectRatio / currentAspectRatio);
        }

        // Apply the new orthographic size to the camera
        Camera.main.orthographicSize = orthographicSize;
    }
}
