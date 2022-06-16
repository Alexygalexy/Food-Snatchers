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
        if (smallestDistance2 < 3f)
        {
            navMeshAgent.speed += 3f;
            navMeshAgent.destination = -ClosestEnemy.transform.position;
            navMeshAgent.speed -= 3f;
            StartCoroutine("RunAway");
        }
        scoreBoard();

        ClosestFood = FindClosestFood();
        movePositionTransform = ClosestFood;
        base.GoToPosition();

        if (timer > timeStamp)
        {
            vanish.Play();
            cam.GetComponent<MultipleTargetsCamera>().targets.Remove(transform);
            Destroy(transform.gameObject, 0.3f);
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (other.gameObject.name == "RayaBotTest")
            {
                //other.gameObject.GetComponent<AI_System>().Score += Score;
            }
            else
            {
                vanish.Play();
                cam.GetComponent<MultipleTargetsCamera>().targets.Remove(transform);
                Destroy(transform.gameObject, 0.3f);
                Debug.Log("Clone is gone ;(");
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected IEnumerator RunAway()
    {
        yield return new WaitForSecondsRealtime(5f);
    }

}
