using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5;
    private int hp;
    bool facingRight = true; // Assuming the player starts facing right
    [SerializeField] AudioSource killSound;
    [SerializeField] Canvas deathScene;
    [SerializeField] private ParticleSystem deathParticlePrefab; // Reference to the death particle prefab
    [SerializeField] private ParticleSystem damageParticlePrefab; // Reference to the death particle prefab
    private bool isInvincible = false; // Flag to check if the player is currently invincible
    //[SerializeField] private GameObject spawnEffectPrefab; // Reference to the spawn effect prefab
    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


    void Start()
    {
        deathScene.enabled = false;
        hp = 2;
    }
    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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

    private void Update()
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

            if (hp == 1)
            {
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

        }else if (other.CompareTag("enemyHead")){
            killSound.Play();
        }
    }
}