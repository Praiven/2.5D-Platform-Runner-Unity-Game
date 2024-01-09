using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Upgrade : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    Player player;
    Jump playerJump; // Reference to the Jump script

    private void Start()
    {
        player = playerObject.GetComponent<Player>();
        playerJump = playerObject.GetComponent<Jump>(); // Get the Jump script
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Health")) // the tag of the current power up
            {
                player.hp = 2;
                Debug.Log(player.hp);
                player.SmokeDisabler();
            }
            else if (gameObject.CompareTag("Jump"))
            {
                playerJump.EnableDoubleJump(); // Enable double jump
            }
            else if (gameObject.CompareTag("Speed"))
            {
                player.speed = 5;
            }
            Destroy(gameObject);
        }
    }
}
