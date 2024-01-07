using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool movingRight = false;
    float Speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
        }
        else
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            movingRight = !movingRight;
            transform.Rotate(0, 180, 0); // Rotate 180 degrees around the Y-axis
        }
    }
}
