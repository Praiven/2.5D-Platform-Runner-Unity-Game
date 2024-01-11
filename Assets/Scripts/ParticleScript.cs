using System.Collections;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public ParticleSystem particle;     // Variables that define the Particle 
    public Collider fireCollider; 
    public AudioSource audioSource; 
    public AudioClip audioClip; 

    private void Start()
    {
        StartCoroutine(EmitParticles());
    }

    IEnumerator EmitParticles()       // Function that controls when the particles are being emmited 
    {
        while (true)
        {
            particle.Play();
            fireCollider.enabled = true; // Enable the collider
            audioSource.PlayOneShot(audioClip); // Play the audio clip
            yield return new WaitForSeconds(4);
            particle.Stop();
            fireCollider.enabled = false; // Disable the collider
            audioSource.Stop(); // Stop the audio
            yield return new WaitForSeconds(3);
        }
    }
}
