using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGoTo : MonoBehaviour
{
    public string dragonScene = "DragonRoom";
    public string menuScene = "MenuRoom";

    public void DragonRoom()
    {
        SceneManager.LoadScene(dragonScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void End()
    {
        Application.Quit();
    }
}
