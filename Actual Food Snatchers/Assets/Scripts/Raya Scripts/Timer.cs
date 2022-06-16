using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft;    
    public TMP_Text timerText;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Floor(timeLeft / 60).ToString("00") + ":" + (timeLeft % 60).ToString("00"); 
        if (timeLeft < 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
    }
}
