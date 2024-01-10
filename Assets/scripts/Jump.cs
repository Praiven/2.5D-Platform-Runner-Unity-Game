using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] AudioSource killSound;
    [SerializeField] AudioSource jumpSound;
    Rigidbody rigidbody;
    public float jumpStrength = 2.6f;
    public event System.Action Jumped;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;
    bool doubleJump = false;
    int currentJumps = 0;

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
        // Jump when the Jump button is pressed and we haven't reached the max jump count.
        if (Input.GetKeyDown("w") && (!groundCheck || groundCheck.isGrounded || (doubleJump && currentJumps<1)))
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
            jumpSound.Play();
            currentJumps++;


        }
        if (groundCheck.isGrounded)
        {
            currentJumps = 0;
        }

    }

    public void WhenEnemyHeadCheckerTriggerEnter(Collider collider)
    {
        killSound.Play();
        rigidbody.AddForce(Vector3.up * 100 * 2f);
    }



    public void EnableDoubleJump()
    {
       doubleJump = true; // Enable double jump
    }

    public void DisableDoubleJump()
    {
        doubleJump = false; // Disable double jump
    }
}