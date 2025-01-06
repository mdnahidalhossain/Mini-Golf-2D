using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform sprite; // The sprite to rotate
    public float rotationSpeed = 50f; // Speed of rotation (degrees per second)


    void Start()
    {

    }

    void Update()
    {
        if (sprite != null)
        {
            // Rotate the sprite
            sprite.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }
}
