using UnityEngine;
using System.Collections.Generic;

public class TransitionManager : MonoBehaviour
{
    public List<GameObject> gameObjects; // Assign your GameObjects in the inspector
    private int currentIndex = -1; // Start with -1 since no GameObject is active initially

    void Start()
    {
        // Ensure all GameObjects are inactive at the start
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }

    public void OnPlayerScore()
    {
        if (currentIndex >= gameObjects.Count - 1)
        {
            Debug.Log("All GameObjects have been activated. Transition stopped.");
            return; // Stop further transitions
        }

        // Deactivate the current GameObject
        if (currentIndex >= 0 && currentIndex < gameObjects.Count)
        {
            gameObjects[currentIndex].SetActive(false);
        }

        // Increment the index
        currentIndex++;

        // Check if there are more GameObjects to activate
        if (currentIndex < gameObjects.Count)
        {
            gameObjects[currentIndex].SetActive(true);
        }

        
    }
}
