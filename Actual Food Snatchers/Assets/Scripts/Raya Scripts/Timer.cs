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

    public GameObject scoreScreen;

    public GameObject alexBot;
    public GameObject sjoekeBot;
    public GameObject rayaBot;
    public GameObject vitalijBot;
    public GameObject tomBot;

    public TMP_Text alexScore;
    public TMP_Text sjoekeScore;
    public TMP_Text rayaScore;
    public TMP_Text vitalijScore;
    public TMP_Text tomScore;



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
    /// <summary>
    /// 
    /// Using Rayas timer script. Make the scoreboard appear after the time hits 0. 
    /// 
    /// Also showing the current scores of the players
    /// 
    /// -Alex
    /// 
    /// </summary>
    void GameOver()
    {
        Time.timeScale = 0;
        scoreScreen.SetActive(true);

        alexScore.text = "Alex: " + alexBot.GetComponent<Alex.Alex_Bot>().Score.ToString();
        sjoekeScore.text = "Sjoeke: " + sjoekeBot.GetComponent<Sjoeke_AI>().Score.ToString();
        rayaScore.text = "Raya: " + rayaBot.GetComponent<RayaBot>().Score.ToString();
        vitalijScore.text = "Vitalij: " + vitalijBot.GetComponent<CleanVitalijBot>().Score.ToString();
        tomScore.text = "Tom: " + tomBot.GetComponent<Tom_Bot>().Score.ToString();


    }
}
