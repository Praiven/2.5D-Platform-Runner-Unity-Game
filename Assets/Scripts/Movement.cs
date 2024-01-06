using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    float Speed = 3;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * Speed * Time.deltaTime;
        }
        //if (Input.GetKey(KeyCode.W) && isGrounded)
        //{

            //rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            //isGrounded = false;
        //}
    }
}

