using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] AudioSource killSound;
    [SerializeField] AudioSource jumpSound;
    [SerializeField] GroundCheck groundCheck;
    Rigidbody rigidBody;
    public float jumpStrength = 2.6f;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]

    void Awake()
    {
        // Get rigidbody.
        rigidBody = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed and we haven't reached the max jump count.
        if (Input.GetKeyDown("w") && (!groundCheck || groundCheck.isGrounded))
        {
            rigidBody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
            jumpSound.Play();
        }
    }
    public void killJump() // this gets called from the player script when we kill an enemy
    {
        rigidBody.AddForce(Vector3.up * 100 * 2f);
    }
}