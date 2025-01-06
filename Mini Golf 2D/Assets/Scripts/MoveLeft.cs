using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float windSpeed;
    //public Vector2 windAxis = new Vector2(-1, 0);
    //public float xBound = 6.25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * windSpeed * Time.deltaTime);
        DestroyWindObject();
    }

    private void DestroyWindObject()
    {
        float xBound = 6.25f;

        if (transform.position.x < -xBound)
        {
            Destroy(gameObject);
        }
    }
}
