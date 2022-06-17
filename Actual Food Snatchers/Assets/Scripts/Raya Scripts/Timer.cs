using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeLeft;    
    public TMP_Text timerText;

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
        scoreScreen.SetActive(true);

        alexScore.text = "Alex: " + alexBot.GetComponent<Alex.Alex_Bot>().Score.ToString();
        sjoekeScore.text = "Sjoeke: " + sjoekeBot.GetComponent<Sjoeke_AI>().Score.ToString();
        rayaScore.text = "Raya: " + rayaBot.GetComponent<RayaBot>().Score.ToString();
        vitalijScore.text = "Vitalij: " + vitalijBot.GetComponent<CleanVitalijBot>().Score.ToString();
        tomScore.text = "Tom: " + tomBot.GetComponent<Tom_Bot>().Score.ToString() + 1;


    }
}
