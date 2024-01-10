using System.Collections;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Collider fireCollider; // Add this line
    public AudioSource audioSource; // Add this line for the audio source
    public AudioClip audioClip; // Add this line for the audio clip

    private void Start()
    {
        StartCoroutine(EmitParticles());
    }

    IEnumerator EmitParticles()
    {
        while (true)
        {
            particleSystem.Play();
            fireCollider.enabled = true; // Enable the collider
            audioSource.PlayOneShot(audioClip); // Play the audio clip
            yield return new WaitForSeconds(4);
            particleSystem.Stop();
            fireCollider.enabled = false; // Disable the collider
            audioSource.Stop(); // Stop the audio
            yield return new WaitForSeconds(3);
        }
    }
}
