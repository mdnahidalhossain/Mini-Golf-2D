using UnityEngine;
using System.Collections;

public class BallController2D : MonoBehaviour
{
    public float maxForce = 20f;         // Maximum force applied to the ball
    public LineRenderer aimLine;        // Line Renderer for visualizing aim
    public LayerMask holeLayer;         // Layer for detecting the hole
    public float velocityThreshold = 0.5f; // Minimum velocity to consider the ball as "moving"
    private bool isAiming = false;
    private bool isBallMoving = false; // Track if the ball is moving

    private Rigidbody2D rb;
    private GameManager gameManager;
    private bool isScaling = false;    // Track if the ball is scaling down
    public float shrinkSpeed = 0.5f;  // Speed of shrinking
    public float moveSpeed = 5f;      // Speed of moving towards the hole

    public GameObject dirtVfx;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>(); // Get reference to GameManager

        // Configure the Line Renderer
        if (aimLine != null)
        {
            aimLine.positionCount = 2;    // Start and end points
            aimLine.enabled = false;     // Hide the line initially
        }
    }

    private IEnumerator OnBallStopped()
    {
        yield return new WaitForSeconds(2.0f);

        // Notify the GameManager about the miss
        //gameManager.SwitchTurn();
        gameManager.SpawnBall();
        Destroy(gameObject);
    }

    void Update()
    {
        // If the ball is moving or scaling, prevent new input for aiming
        if (isBallMoving || isScaling)
        {
            if (rb.velocity.magnitude < velocityThreshold)
            {
                isBallMoving = false;
                StartCoroutine(OnBallStopped());
            }
            return;
        }

        // Start aiming
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
            if (aimLine != null)
                aimLine.enabled = true; // Show the aim line
        }

        // Update aim direction
        if (Input.GetMouseButton(0) && isAiming)
        {
            Vector2 aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
            float distance = Mathf.Clamp(aimDirection.magnitude, 0, 1.0f);
            aimDirection.Normalize();

            // Update Line Renderer positions
            if (aimLine != null)
            {
                aimLine.SetPosition(0, rb.position); // Start point at the ball
                aimLine.SetPosition(1, rb.position - aimDirection * distance); // End point based on aim direction and force
            }
        }

        // Apply force when mouse is released
        if (Input.GetMouseButtonUp(0) && isAiming)
        {
            isAiming = false;
            if (aimLine != null)
                aimLine.enabled = false; // Hide the aim line

            Vector2 aimDirection = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
            float distance = Mathf.Clamp(aimDirection.magnitude, 5, maxForce); // Get distance from the ball to the mouse
            float force = distance; // Use the distance as the force

            // Apply the force to the ball
            rb.AddForce(-aimDirection.normalized * force, ForceMode2D.Impulse);
            isBallMoving = true; // Ball is now moving
            Debug.Log("Ball is now moving.");
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & holeLayer) != 0) // Ball enters the hole
        {
            if (dirtVfx != null)
            {
                Instantiate(dirtVfx, transform.position, Quaternion.identity); // Instantiate the VFX at the ball's position
            }

            Debug.Log("Hole in one! Ball has entered the hole.");
            if (!isScaling)
            {
                StartCoroutine(MoveAndShrinkToHole(collision.transform));
                gameManager.AddScore(1);
            }
        }
    }

    private IEnumerator MoveAndShrinkToHole(Transform hole)
    {
        isScaling = true;

        // Disable ball's physics
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;

        while (transform.localScale.magnitude > 0.01f)
        {
            // Smoothly move the ball towards the hole
            transform.position = Vector3.Lerp(transform.position, hole.position, moveSpeed * Time.deltaTime);

            // Smoothly shrink the ball
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, shrinkSpeed * Time.deltaTime);

            yield return null;
        }

        // Ensure the ball is completely gone
        transform.localScale = targetScale;
        transform.position = hole.position;

        // Destroy the ball and reset it
        Destroy(gameObject);
        gameManager.ResetBallAfterDelay();
    }
}
