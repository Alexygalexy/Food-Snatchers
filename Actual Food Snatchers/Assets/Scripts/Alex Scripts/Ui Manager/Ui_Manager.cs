using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Ui Scene controller to transition through scenes
/// 
/// -Alex
/// </summary>
public class Ui_Manager : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("The_Final_Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
