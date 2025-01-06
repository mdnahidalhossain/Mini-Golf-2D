using UnityEngine;

public class LeftAndRightScaling : MonoBehaviour
{
    public Transform leftSprite;  // Rectangle on the left side (scale starts at 1)
    public Transform rightSprite; // Rectangle on the right side (scale starts at 0.5)

    public float scaleSpeed = 2f; // Speed of scaling
    public float maxScale = 1.5f; // Maximum scale on x-axis for both
    public float minScale = 0.5f; // Minimum scale on x-axis for both

    private Vector3 leftOriginalScale;
    private Vector3 rightOriginalScale;

    private Vector3 leftOriginalPosition;
    private Vector3 rightOriginalPosition;

    void Start()
    {
        // Store the original scale and position of both rectangles
        if (leftSprite != null)
        {
            leftOriginalScale = leftSprite.localScale;
            leftOriginalPosition = leftSprite.position;
        }

        if (rightSprite != null)
        {
            rightOriginalScale = rightSprite.localScale;
            rightOriginalPosition = rightSprite.position;
        }
    }

    void Update()
    {
        // Calculate the scaling factor using Mathf.PingPong
        float scaleFactor = Mathf.PingPong(Time.time * scaleSpeed, maxScale - minScale) + minScale;

        if (leftSprite != null)
        {
            // Scale the left sprite decreasing from right to left, keeping the left side anchored
            leftSprite.localScale = new Vector3(scaleFactor, leftOriginalScale.y, leftOriginalScale.z);
            float leftOffset = (scaleFactor - leftOriginalScale.x) / 2f;
            leftSprite.position = new Vector3(leftOriginalPosition.x + leftOffset, leftOriginalPosition.y, leftOriginalPosition.z);
        }

        if (rightSprite != null)
        {
            // Scale the right sprite increasing from right to left, keeping the right side anchored
            float inverseScaleFactor = maxScale + minScale - scaleFactor; // Inverse scaling
            rightSprite.localScale = new Vector3(inverseScaleFactor, rightOriginalScale.y, rightOriginalScale.z);
            float rightOffset = (inverseScaleFactor - rightOriginalScale.x) / 2f;
            rightSprite.position = new Vector3(rightOriginalPosition.x - rightOffset, rightOriginalPosition.y, rightOriginalPosition.z);
        }
    }
}
