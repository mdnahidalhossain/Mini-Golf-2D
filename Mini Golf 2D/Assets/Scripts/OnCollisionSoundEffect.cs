using UnityEngine;

public class OnCollisionSoundEffect : MonoBehaviour
{
    private AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip holeSound;       // Sound effect for when the ball enters the hole

    void Start()
    {
        // Get the AudioSource component attached to the hole (if it exists)
        audioSource = GetComponent<AudioSource>();
    }

    // This method will be called when something collides with the hole
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the other object that collided with the hole is the ball
        if (other.gameObject)  // Assuming your ball has the "Ball" tag
        {
            // Play the hole sound effect when the ball collides with the hole
            if (audioSource != null && holeSound != null)
            {
                audioSource.PlayOneShot(holeSound);  // Play the sound once
            }

            // Additional logic (e.g., respawning the ball) can be handled in BallController or here.
        }
    }
}
