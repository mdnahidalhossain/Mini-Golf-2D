using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;       // Reference to the ball prefab
    public GameObject holePrefab;

    public Vector2 initialBallPosition; // The initial position of the ball
    public Vector2 initialHolePosition; // The initial position of the hole
    public float resetDelay = 2f;       // Delay before respawning the ball

    private int score = 0;              // Player's score
    private GameObject currentHole;     // Reference to the current hole

    public TransitionManager transitionManager;
    public Text playerScoreText;

    private void Start()
    {
        SpawnHole();
        SpawnBall();
    }

    public void SpawnBall()
    {
        Instantiate(ballPrefab, initialBallPosition, Quaternion.identity);
    }

    public void SpawnHole()
    {
        // Instantiate the hole at the initial position and store the reference
        currentHole = Instantiate(holePrefab, initialHolePosition, Quaternion.identity);
    }

    public void ResetBallAfterDelay()
    {
        StartCoroutine(ResetBallCoroutine());
    }

    private IEnumerator ResetBallCoroutine()
    {
        yield return new WaitForSeconds(resetDelay);
        SpawnBall();
    }

    public void AddScore(int points)
    {
        // Increment the player's score
        score += points;
        playerScoreText.text = "Score: " + score.ToString();
        Debug.Log("Score: " + score);

        StartCoroutine(TransitionOnDelay());
    }

    private IEnumerator TransitionOnDelay()
    {
        yield return new WaitForSeconds(2.0f);

        transitionManager.OnPlayerScore();
    }


}
