using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalScript : MonoBehaviour
{
    [SerializeField] AudioSource portalSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            portalSound.Play();
            StartCoroutine(AudioEffect(1.3f));
        }
    }

    private IEnumerator AudioEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(SceneManager.GetActiveScene().buildIndex + 1 == 4)
        {
            SceneManager.LoadScene("StartingMenu");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        }
    }
}
