using System.Collections;
using UnityEngine;

public class CharacterTransparency : MonoBehaviour
{
    public float duration = 10f; // Duration for the transparency change

    void Start()
    {
        StartCoroutine(LerpTransparency());
    }

    IEnumerator LerpTransparency()
    {
        float timeElapsed = 0;

        Material material = GetComponent<Renderer>().material;
        Color originalColor = material.color;
        Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 1); // Less transparent

        while (timeElapsed < duration)
        {
            material.color = Color.Lerp(originalColor, targetColor, timeElapsed / duration);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        material.color = targetColor; // Ensure the final color is set correctly
    }
}