using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayaBotClone : RayaBot
{
    protected AudioSource vanish;

    // Start is called before the first frame update
    protected override void Start()
    {
        ScoreBoard = transform.Find("ScoreBoard_Raya_Clone").gameObject;
        player1_scoreText = ScoreBoard.GetComponentInChildren<TextMeshProUGUI>();

        Score = transform.parent.gameObject.GetComponent<RayaBot>().Score;
        player1_scoreText.text = Score.ToString();

        audioRaya = GetComponents<AudioSource>();
        collect = audioRaya[0];
        vanish = audioRaya[1];
        vanish.Play();

        timeStamp = Time.deltaTime + 15f;

        cam = GameObject.FindObjectOfType<Camera>().transform;
        cam.GetComponent<MultipleTargetsCamera>().targets.Add(this.transform);
    }

    // Update is called once per frame
    protected override void Update()
    {
        timer += Time.deltaTime;

        ClosestEnemy = FindClosestEnemy();
        if (smallestDistance2 < 5f)
        {
            
            StartCoroutine("RunAway");
        }
        scoreBoard();

        ClosestFood = FindClosestFood();
        movePositionTransform = ClosestFood;
        base.GoToPosition();

        if (timer > timeStamp)
        {
            timeStamp += 60f;
            vanish.Play();
            cam.GetComponent<MultipleTargetsCamera>().targets.Remove(transform);
            Destroy(transform.gameObject, 0.2f);
            transform.parent.gameObject.GetComponent<RayaBot>().Score *= 2;
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player") && other.gameObject.name != "RayaBotTest")
        {
            vanish.Play();
            cam.GetComponent<MultipleTargetsCamera>().targets.Remove(transform);
            Destroy(transform.gameObject, 0.2f);
            Debug.Log("Clone is gone ;(");
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected IEnumerator RunAway()
    {
        navMeshAgent.speed += 3f;
        navMeshAgent.destination = -ClosestEnemy.transform.position;
        navMeshAgent.speed -= 3f;
        yield return new WaitForSecondsRealtime(5f);
    }

}
