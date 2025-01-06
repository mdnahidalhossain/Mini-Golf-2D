using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float windSpeed;
    public Vector2 windAxis = new Vector2 (1,0);
    public float xBound = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(windAxis * windSpeed *  Time.deltaTime);
        DestroyWindObject();
    }

    private void DestroyWindObject()
    {
        if (transform.position.x > xBound)
        {
            Destroy(gameObject);
        }
    }
}
