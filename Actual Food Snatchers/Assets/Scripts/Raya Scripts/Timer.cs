//Manages the timer, sounds and score board

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft;    
    public TMP_Text timerText;
    bool playCountdown = false;

    public AudioSource countdown;

    private void Start()
    {
        countdown = GetComponent<AudioSource>();
    }
    void Update()
    {
        timeLeft -= Time.deltaTime;

        //formats the timer view
        timerText.text = Mathf.Floor(timeLeft / 60).ToString("00") + ":" + (timeLeft % 60).ToString("00"); 

        //plays countdown audio
        if(timeLeft < 10.5f && playCountdown == false)
        {
            playCountdown = true;
            countdown.Play();
        }

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
