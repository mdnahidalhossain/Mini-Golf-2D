using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawnManager : MonoBehaviour
{
    [SerializeField] public GameObject windPrefab;

    private float timeDelay = 0f;
    private float spawnTime = 2.0f;
    private bool isSpawning = false;

    public float posX = -4.1f;
    //private float posY = Random.Range(0.8f, -0.09f);

    private void OnEnable()
    {
        // Start spawning wind images when this GameObject is activated
        isSpawning = true;
        InvokeRepeating(nameof(SpawnPosition), timeDelay, spawnTime);
    }

    private void OnDisable()
    {
        // Stop spawning wind images when this GameObject is deactivated
        isSpawning = false;
        CancelInvoke(nameof(SpawnPosition));
    }


    public void SpawnPosition()
    {
        //float posX = -4.1f;
        float posY = Random.Range(0.8f, -0.09f);

        Vector2 windSpawnPos = new Vector2(posX, posY);

        Instantiate(windPrefab, windSpawnPos, windPrefab.transform.rotation);
    }

    

}
