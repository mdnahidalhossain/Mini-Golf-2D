using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public Vector2 windDirection = new Vector2(1f, 0.5f); // Direction of the wind
    public float windStrength = 2f; // Base strength of the wind
    public float gustFrequency = 1f; // Frequency of wind gusts
    public float gustVariation = 0.5f; // Variation in wind strength (for gust effect)

    public float minY = -5f; // Minimum y-boundary for wind effect
    public float maxY = 5f;  // Maximum y-boundary for wind effect

    private Vector2 currentWindForce;

    void Start()
    {
        windDirection.Normalize(); // Normalize the wind direction vector
    }

    void FixedUpdate()
    {
        // Calculate fluctuating wind strength using a sine wave for gust effect
        float gustFactor = windStrength + Mathf.Sin(Time.time * gustFrequency) * gustVariation;

        // Calculate the current wind force
        currentWindForce = windDirection * gustFactor;

        // Apply the wind force to objects within the y-boundaries
        ApplyWindForce();
    }

    private void ApplyWindForce()
    {
        // Find all Rigidbody2D objects in the scene
        Rigidbody2D[] objects = FindObjectsOfType<Rigidbody2D>();

        foreach (Rigidbody2D rb in objects)
        {
            // Check if the object is within the y-boundaries
            if (rb.position.y >= minY && rb.position.y <= maxY)
            {
                // Apply wind force if within boundaries
                rb.AddForce(currentWindForce, ForceMode2D.Force);
            }
        }
    }
}
