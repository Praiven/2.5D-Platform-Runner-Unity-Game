using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour        // This script is a component of the kill point game object, that gets triggered when the player steps on the head of the enemy
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private ParticleSystem deathParticlePrefab; // Reference to the death particle prefab

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))      // Tag to identify that the player is above an enemy's head
        {
            // Instantiate the death particle at the enemy's position and rotation
            ParticleSystem deathParticle = Instantiate(deathParticlePrefab, enemy.transform.position, enemy.transform.rotation);
            // Play the particle system once
            deathParticle.Play();

            // Destroy the enemy
            Destroy(enemy);
                
            // Destroy the particle system after it has finished playing
            Destroy(deathParticle.gameObject, deathParticle.main.duration);
        }
    }
}

