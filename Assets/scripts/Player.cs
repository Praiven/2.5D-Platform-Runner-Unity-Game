using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    private int hp;
    float Speed = 3;
    public Vector3 jump;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    Rigidbody rb;
    bool facingRight = true; // Assuming the player starts facing right
    [SerializeField] Canvas deathScene;
    [SerializeField] private ParticleSystem deathParticlePrefab; // Reference to the death particle prefab
    [SerializeField] private ParticleSystem damageParticlePrefab; // Reference to the death particle prefab
    private bool isInvincible = false; // Flag to check if the player is currently invincible
    //[SerializeField] private GameObject spawnEffectPrefab; // Reference to the spawn effect prefab

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        deathScene.enabled = false;
        hp = 2;
        // Instantiate the spawn effect at the player's position and rotation
        //GameObject spawnEffect = Instantiate(spawnEffectPrefab, transform.position, transform.rotation);

        // Destroy the spawn effect after it has finished playing
        // Replace 'duration' with the length of your animation
        //Destroy(spawnEffect, spawnEffect.GetComponent<ParticleSystem>().main.duration);
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

    private IEnumerator Invincibility(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvincible)
        {
            StartCoroutine(Invincibility(1f)); // Make the player invincible for 1 second

            if (hp == 1) { 
                // Instantiate the death particle at the player's position and rotation
                ParticleSystem deathParticle = Instantiate(deathParticlePrefab, transform.position, transform.rotation);

                // Play the particle system once
                deathParticle.Play();

                // Disable emission of the particle system
                var emission = deathParticle.emission;
                emission.enabled = false;

                // Destroy the particle system after it has finished playing
                Destroy(deathParticle.gameObject, deathParticle.main.duration);

                // Destroy the player
                Destroy(gameObject);
                deathScene.enabled = true;
                Time.timeScale = 0f;
                Debug.Log("Test");
            }
            if (hp == 2)
            {
                // Instantiate the damage particle at the player's position and rotation
                ParticleSystem damageParticle = Instantiate(damageParticlePrefab, transform.position, transform.rotation);
                // Attach the damage particle to the player
                damageParticle.transform.parent = transform;

                // Start playing the damage particle
                damageParticle.Play();
                hp = 1;
            }

        }
    }
}

