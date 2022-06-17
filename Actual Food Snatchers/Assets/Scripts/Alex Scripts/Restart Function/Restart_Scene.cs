using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart_Scene : MonoBehaviour
{
    //Activating the time scale back
    private void Awake()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// 
    /// Restarting the current scene to restart the game
    /// 
    /// -Alex
    /// 
    /// </summary>
    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
