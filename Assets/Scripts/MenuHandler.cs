using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Chapter1");
    }

    public void Back()
    {
        SceneManager.LoadScene("Scenes/MainMenu");
    }

    public void About()
    {
        SceneManager.LoadScene("Scenes/About");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
