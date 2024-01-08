using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainSceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        if(Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadSceneAsync("StartingMenu");

    }

    public void Restart()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
        SceneManager.LoadSceneAsync("MainScene");
    }
}
