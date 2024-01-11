using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour     // This script is enabled when the user enters the Portal 
{
    [SerializeField] AudioSource portalSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))      // When the player enters the portal
        {
            portalSound.Play();
            StartCoroutine(AudioEffect(1.3f));      // Calls a coroutine to delay the transition, until the audio effect has been played 
        }
    }

    private IEnumerator AudioEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(SceneManager.GetActiveScene().buildIndex + 1 == 4)        // If the current scene is the last stage, navigate to the main menu 
        {
            SceneManager.LoadScene("StartingMenu");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
}
