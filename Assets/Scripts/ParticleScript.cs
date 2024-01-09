using System.Collections;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public Collider fireCollider; // Add this line

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
            yield return new WaitForSeconds(4);
            particleSystem.Stop();
            fireCollider.enabled = false; // Disable the collider
            yield return new WaitForSeconds(3);
        }
    }
}
