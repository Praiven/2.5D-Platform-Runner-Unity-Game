using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour     // This script is used when the user dies and the menu pops up
{
    public void LoadMainMenu()
    {
        if(Time.timeScale == 0f)     // Checks if the game was frozen 
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
