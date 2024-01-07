using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    float Speed = 3;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;
    bool facingRight = true; // Assuming the player starts facing right

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
        if (Input.GetKeyDown(KeyCode.D) && !facingRight)
        {
            facingRight = true;
            transform.Rotate(0, 180, 0); // Rotate 180 degrees around the Y-axis
        }
        else if (Input.GetKeyDown(KeyCode.A) && facingRight)
        {
            facingRight = false;
            transform.Rotate(0, 180, 0); // Rotate 180 degrees around the Y-axis
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.position -= transform.right * Speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.W) && isGrounded)
        {

            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("you died MAN");
        }
    }
}

