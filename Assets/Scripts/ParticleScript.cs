using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public ParticleSystem particleSystem;

    private void Start()
    {
        StartCoroutine(EmitParticles());
    }

    IEnumerator EmitParticles()
    {
        while (true)
        {
            particleSystem.Play();
            yield return new WaitForSeconds(4);
            particleSystem.Stop();
            yield return new WaitForSeconds(3);
        }
    }
}
