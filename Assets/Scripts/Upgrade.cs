using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Upgrade : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    private Player player;   // Reference to the Player script 
    private Jump playerJump; // Reference to the Jump script

    private void Start()
    {
        player = playerObject.GetComponent<Player>();   // Get the Player script 
        playerJump = playerObject.GetComponent<Jump>(); // Get the Jump script
    }

    private void OnTriggerEnter(Collider other)         // When the player interacts with a Powerup 
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Health")) // If the tag of the current gameObject(Powerup) is health, set hp to 2 and disable the smoke(smoke indicates if the player was damaged)
            {
                player.hp = 2;
                player.SmokeDisabler();
            }
            else if (gameObject.CompareTag("Jump"))
            {
                playerJump.jumpStrength = 4f; // Increase jump strength
            }
            else if (gameObject.CompareTag("Speed"))
            {
                player.speed = 5;         // Increase speed
            }
            Destroy(gameObject);          // The current power up is destroyed, because it is consumed by the player
        }
    }
}
