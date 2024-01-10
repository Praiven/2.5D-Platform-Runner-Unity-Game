using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private bool shield;
    public int hp;
    bool facingRight = true; // Assuming the player starts facing right
    [SerializeField] MeshRenderer cylinder;
    [SerializeField] MeshRenderer chamferbox;
    [SerializeField] MeshRenderer helix;
    [SerializeField] MeshRenderer cylinder007;
    [SerializeField] AudioSource dmgTaken;
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource powerUpEffect;
    [SerializeField] Canvas deathScene;
    [SerializeField] private ParticleSystem deathParticlePrefab; // Reference to the death particle prefab
    [SerializeField] private ParticleSystem damageParticlePrefab; // Reference to the death particle prefab
    private bool isInvincible = false; // Flag to check if the player is currently invincible
    //[SerializeField] private GameObject spawnEffectPrefab; // Reference to the spawn effect prefab
    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    ParticleSystem damageParticle;


    void Start()
    {
        deathScene.enabled = false;
        InitializePlayer();
    }
    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        HandlePlayerRotation();
    }

    private IEnumerator Invincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;

    }

    private IEnumerator EnableCanvasAfterFrames(int frameCount)
    {
        for (int i = 0; i < frameCount; i++)
        {
            yield return null; // wait for one frame
        }
        deathScene.enabled = true;
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvincible)
        {
            StartCoroutine(Invincibility(1f)); // Make the player invincible for 1 second
            if (hp == 1)
            {
                HandlePlayerDeath();
            }
            if (hp == 2)
            {
                HandlePlayerDamage();
            }
            if (shield)
            {
                dmgTaken.Play();
                shield = false;
                Color newColor = new Color(255f / 255f, 255f / 255f, 255f / 255f, 1); // Set this to the color you want 
                Transform childTransform = transform.Find("Cylinder006/ChamferBox001");
                Renderer childRenderer = childTransform.GetComponent<Renderer>();
                childRenderer.material.color = newColor;
                float bounceForce = 30f; // Adjust this value to change the strength of the bounce
                Vector3 bounceDirection = facingRight ? Vector3.left : Vector3.right;
                rigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
                hp = 2;
            }
            if (speed != 3)
            {
                dmgTaken.Play();
                speed = 3;
            }
            dmgTaken.Play();
            GetComponent<Jump>().DisableDoubleJump();
        }
        else if (other.CompareTag("Barrier"))
        {
            HandlePlayerDeath();
        }
        else if (other.CompareTag("Health"))
        {
            powerUpEffect.Play();
        }
        else if (other.CompareTag("Shield"))
        {
            powerUpEffect.Play();
            Shield();
            shield = true;
        }
        else if (other.CompareTag("Speed"))
        {
            powerUpEffect.Play();
        }
        else if (other.CompareTag("Jump"))
        {
            powerUpEffect.Play();
        }
    }

    private void Shield()
    {
        Color newColor = new Color(255f / 255f, 122f / 255f, 0f / 255f, 1); // Set this to the color you want 
        Transform childTransform = transform.Find("Cylinder006/ChamferBox001");
        Renderer childRenderer = childTransform.GetComponent<Renderer>();
        childRenderer.material.color = newColor;
        hp = 3; 
    }
 
    private void  HandlePlayerDamage()
    {
        float bounceForce = 30f; // Adjust this value to change the strength of the bounce
        Vector3 bounceDirection = facingRight ? Vector3.left : Vector3.right;
        rigidbody.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        // Start playing the damage particle
        dmgTaken.Play();
        hp = 1;
        SmokeDisabler();
    }

    private void InitializePlayer()
    {
        hp = 2;
        // Instantiate the damage particle at the player's position and 
        damageParticle = Instantiate(damageParticlePrefab, transform.position , transform.rotation);
        // Attach the damage particle to the player
        damageParticle.transform.parent = transform;
        damageParticle.Stop();
    }

    public void SmokeDisabler()
    {
        if (hp == 2)
        {
            damageParticle.Stop();
        }
        else if (hp == 1)
        {
            damageParticle.Play();
        }

    }

    private void HandlePlayerRotation()
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
    }

    private void MovePlayer()
    {
        // Get targetMovingSpeed.
        float targetMovingSpeed = speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides.Count - 1;
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(0, -Input.GetAxis("Horizontal") * targetMovingSpeed);

        // Apply movement.
        if (facingRight)
        {
            rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.y, rigidbody.velocity.y, targetVelocity.x);
        }
        else
        {
            rigidbody.velocity = transform.rotation * new Vector3(-targetVelocity.y, rigidbody.velocity.y, -targetVelocity.x);
        }
    }

    private void HandlePlayerDeath()
    {
        deathSound.Play();
        // Instantiate the death particle at the player's position and rotation
        ParticleSystem deathParticle = Instantiate(deathParticlePrefab, transform.position, transform.rotation);

        // Play the particle system once
        deathParticle.Play();

        // Disable emission of the particle system
        var emission = deathParticle.emission;
        emission.enabled = false;

        // Destroy the particle system after it has finished playing
        Destroy(deathParticle.gameObject, deathParticle.main.duration);

        //     Destroy the player
        cylinder.enabled = false;
        chamferbox.enabled = false;
        helix.enabled = false;
        cylinder007.enabled = false;
        StartCoroutine(EnableCanvasAfterFrames(60)); // Enable the canvas after 5 frames
    }
}