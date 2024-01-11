using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLoader : MonoBehaviour     // When this script is enabled, the user is transferred to the next scene(Story Telling 1 -> Stage 1)
{
    void OnEnable()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}
