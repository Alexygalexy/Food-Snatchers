//Raya's special ability
//Sub-class of Raya's AI class
//Manages the clone prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayaBotClone : RayaBot
{
    protected AudioSource vanish;
    protected AudioSource baby;
    protected int parentScore;
    protected bool stop = false;

    protected override void Start()
    {
        //gets the score board
        ScoreBoard = transform.Find("ScoreBoard_Raya_Clone").gameObject;
        player1_scoreText = ScoreBoard.GetComponentInChildren<TextMeshProUGUI>();

        timerText = transform.parent.gameObject.GetComponent<RayaBot>().timerText;

        //divides the points between parent and child
        DividePoints();

        //audio clips
        audioRaya = GetComponents<AudioSource>();
        collect = audioRaya[0];
        vanish = audioRaya[1];
        baby = audioRaya[2];
        baby.Play();

        //set lifetime
        timeStamp = 15f;

        //adds camera tracking 
        cam = GameObject.FindObjectOfType<Camera>().transform;
        cam.GetComponent<MultipleTargetsCamera>().targets.Add(transform);
    }

    protected override void Update()
    {
        timer += Time.deltaTime;
        player1_scoreText.text = Score.ToString();
        scoreBoard();

        //defense - avoiding other players
        ClosestEnemy = FindClosestEnemy();
        if (smallestDistanceEnemy < 40f)
        {
            StartCoroutine("RunAway");
        }

        //food collection
        ClosestFood = FindClosestFood();
        movePositionTransform = ClosestFood;
        base.GoToPosition();

        //natural death timer
        if (timer > timeStamp)
        {
            StartCoroutine("CloneVanish");
        }

        //end of game - clone vanishes
        EndGameScore();
    }

    //divides the points between parent and child
    protected void DividePoints()
    {
        parentScore = transform.parent.gameObject.GetComponent<RayaBot>().Score;

        //checks for odd numbers - parent gets more
        if (parentScore % 2 == 1)
        {
            Score = parentScore / 2;
            transform.parent.gameObject.GetComponent<RayaBot>().Score = 1 + parentScore / 2;
        }
        else
        {
            transform.parent.gameObject.GetComponent<RayaBot>().Score = parentScore / 2;
            Score = parentScore / 2;
        }

        player1_scoreText.text = Score.ToString();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    //getting killed
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.gameObject.name != "Raya_Bot")
        {
            vanish.PlayDelayed(0.9f);
            player1_scoreText.color = Color.red;
            cam.GetComponent<MultipleTargetsCamera>().targets.Remove(transform);
            Destroy(transform.gameObject, 1f);
        }
    }

    //natural death
    protected IEnumerator CloneVanish()
    {
        timeStamp += 5f;
        vanish.Play();

        //get invisible 
        transform.localScale = new Vector3(0, 0, 0);

        //give the points to the parent
        transform.parent.gameObject.GetComponent<AI_System>().Score += Score;
        transform.parent.gameObject.GetComponent<AI_System>().player1_scoreText.color = Color.green;

        //stop tracking with camera
        cam.GetComponent<MultipleTargetsCamera>().targets.Remove(transform);

        yield return new WaitForSecondsRealtime(2f);

        transform.parent.gameObject.GetComponent<AI_System>().player1_scoreText.color = Color.white;
        Destroy(transform.gameObject, 2f);
    }

    //no clones at the end of the game
    protected void EndGameScore()
    {
        if (timerText.GetComponent<Timer>().timeLeft < 1.5f && stop == false)
        {
            stop = true;
            StartCoroutine("CloneVanish");
        }
    }
}
