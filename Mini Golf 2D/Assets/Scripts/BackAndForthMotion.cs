using UnityEngine;

public class BackAndForthMotion : MonoBehaviour
{
    public float scaleSpeed = 2f; // Speed of scaling
    public float maxScale = 1.5f; // Maximum scale on x-axis
    public float minScale = 0.5f; // Minimum scale on x-axis

    private Vector3 originalScale;

    void Start()
    {
        // Store the original scale of the sprite
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Calculate the new x-scale using Mathf.PingPong
        float scaleFactor = Mathf.PingPong(Time.time * scaleSpeed, maxScale - minScale) + minScale;

        // Apply the new scale to the x-axis, keeping y and z unchanged
        transform.localScale = new Vector3(scaleFactor, originalScale.y, originalScale.z);
    }
}
