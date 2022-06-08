using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CleanVitalijBot : AI_System, IPauseSystem
{

    private bool collided;
    private bool steal;
    public GameObject[] players;
    [SerializeField] float timer = 5.0f;
    [SerializeField] private float range = 5;
    protected override void Awake()

    {
        base.Awake();
        steal = true;
        collided = true;

    }

    protected void GetClosestPlayer()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in players)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if(currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
    }
    protected override void Update()
    {
        base.Update();
        DetectEnemy();
        if (!steal && !collided)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                steal = true;
                collided = true;

            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    public virtual void DetectEnemy()
    {
        Vector3 detectEnemy = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(detectEnemy * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(detectEnemy * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Player")
            {
                if (collided)
                {
                    if (hit.collider.gameObject.GetComponent<AI_System>().Score > 5)
                    {
                        Debug.Log("STOLEN");

                        hit.collider.gameObject.GetComponent<AI_System>().Score -= 5;
                        hit.collider.gameObject.GetComponent<AI_System>().player1_scoreText.text = hit.collider.gameObject.GetComponent<AI_System>().Score.ToString();
                        this.gameObject.GetComponent<CleanVitalijBot>().Score += 5;
                        this.gameObject.GetComponent<CleanVitalijBot>().player1_scoreText.text = Score.ToString();
                    }
                    else
                    {
                        Debug.Log("There's nothing to steal!");
                    }
                    timer = 5.0f;
                    steal = false;
                    collided = false;

                }
            }
        }
    }

    #region Reusable Methods

    public override void GoToPosition()
    {
        base.GoToPosition();
    }
    protected override void scoreBoard()
    {
        base.scoreBoard();
    }
    #endregion

    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }
}
