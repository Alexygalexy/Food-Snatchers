using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CleanVitalijBot : AI_System, IPauseSystem
{

    private bool collided;
    private bool steal;
    public Transform closestPlayer;
    public Transform closestFood;
    public bool playerContact;
    public AudioSource stealSound;
    public AudioSource eatFood;

    [SerializeField] float timer = 5.0f;
    [SerializeField] private float range = 5;

    // void Start()
    // {
        
    // }

    protected override void Awake()
    {
        closestPlayer = null;
        closestFood = null;
        playerContact = false;
        base.Awake();
        steal = true;
        collided = true;
    }

    // https://www.youtube.com/watch?v=VH-bUST_w0o
    protected Transform GetClosestPlayer()
    {
        List<GameObject> playerObjects = new List<GameObject>();
        playerObjects.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        playerObjects.Remove(transform.gameObject);
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in playerObjects)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;

                trans = go.transform;
            }
        }
        return trans;
    }

    protected Transform GetClosestFood()
    {
        List<GameObject> foodObjects = new List<GameObject>();
        foodObjects.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        foodObjects.AddRange(GameObject.FindGameObjectsWithTag("Chicken"));
        foodObjects.AddRange(GameObject.FindGameObjectsWithTag("Egg"));
        foodObjects.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in foodObjects)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }

    protected override void Update()
    {
        ChaseFood();
        base.Update();
        DetectEnemy();
        GetClosestFood();
        GetClosestPlayer();
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
        eatFood.Play();
        ChaseFood();
        if (other.isTrigger != true && other.CompareTag("Player"))
        {
            closestPlayer = GetClosestPlayer();
            playerContact = true;
            movePositionTransform = closestPlayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger != true && other.CompareTag("Player"))
        {
            playerContact = false;
        }
    }

    private void ChaseFood()
    {
        if(!playerContact)
        {
            closestFood = GetClosestFood();
            movePositionTransform = closestFood;
        }
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }
    
    // https://www.youtube.com/watch?v=E6bac9YP6Jc
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
                    if (hit.collider.gameObject.GetComponent<AI_System>().Score >= 5)
                    {
                        Debug.Log("STOLEN");
                        hit.collider.gameObject.GetComponent<AI_System>().Score -= 5;
                        hit.collider.gameObject.GetComponent<AI_System>().player1_scoreText.text = hit.collider.gameObject.GetComponent<AI_System>().Score.ToString();
                        this.gameObject.GetComponent<CleanVitalijBot>().Score += 5;
                        this.gameObject.GetComponent<CleanVitalijBot>().player1_scoreText.text = Score.ToString();
                        stealSound.Play();
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

    public void Pause(float Speed)
    {
        navMeshAgent.speed = Speed;
    }
}
