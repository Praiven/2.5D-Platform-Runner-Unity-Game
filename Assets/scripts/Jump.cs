﻿using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] AudioSource killSound;
    [SerializeField] AudioSource jumpSound;
    Rigidbody rigidbody;
    public float jumpStrength = 2.6f;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we are on the ground.
        if (Input.GetKeyDown("w") && (!groundCheck || groundCheck.isGrounded))
        {
            Debug.Log("can jump");
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
            jumpSound.Play();
        }
    }
    public void WhenEnemyHeadCheckerTriggerEnter(Collider collider)
    {
        Debug.Log("trigger on jump entered");
        killSound.Play();
        rigidbody.AddForce(Vector3.up * 100 * 2.5f);
    }
}
