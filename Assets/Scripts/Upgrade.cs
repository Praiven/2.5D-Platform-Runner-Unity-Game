using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Upgrade : MonoBehaviour
{
    [SerializeField] GameObject playerObject;
    Player player;

    private void Start()
    {
        player = playerObject.GetComponent<Player>();
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
            Destroy(gameObject);
        }
    }
}
