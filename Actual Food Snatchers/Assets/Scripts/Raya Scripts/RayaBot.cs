using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayaBot : AI_System, IPauseSystem
{
    protected float smallestDistance1;
    protected float smallestDistance2;
    protected Transform ClosestFood;
    protected Transform ClosestEnemy;

    protected float chanceToSnatch;

    protected bool ready = true, pause = false, readyClone = false;

    protected float timer, timeStamp;

    [SerializeField] protected GameObject myPrefab;

    protected AudioSource hit;
    protected AudioSource collect;
    protected AudioSource[] audioRaya;

    public TextMeshProUGUI timerText;

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        audioRaya = GetComponents<AudioSource>();
        hit = audioRaya[0];
        collect = audioRaya[1];

        timeStamp = timer + 10f;
    }


    protected override void Update()
    {
        timer += Time.deltaTime;
        player1_scoreText.text = Score.ToString();

        scoreBoard();
        ClosestFood = FindClosestFood();
        ClosestEnemy = FindClosestEnemy();

        //cool down - not snatching
        if (ready == false)
        {
            if (smallestDistance2 < 15f)
            {
                StartCoroutine("RunAway");
            }
            else
            {
                movePositionTransform = ClosestFood;
                GoToPosition();
            }
        }

        //snatching ready
        else
        {
            if (smallestDistance1 > (smallestDistance2 / 2f))   //prioritize enemies: go after enemy if it's less than 2 times further than the food
            {
                movePositionTransform = ClosestEnemy;
                GoToPosition();
            }
            else
            {
                movePositionTransform = ClosestFood;
                GoToPosition();
            }
        }

        //making a clone
        if (smallestDistance2 > 40f && readyClone == true)
        {
            StartCoroutine("CreateClone");
        }

        //cool down after making a clone
        if (timeStamp < timer && readyClone == false)
        {
            readyClone = true;
        }
    }

    public override void GoToPosition()
    {
        if (movePositionTransform)
        {
            navMeshAgent.destination = movePositionTransform.position;
        }
        else
        {
            navMeshAgent.destination = transform.position;
        }
    }

    //collecting food
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        collect.Play();
    }

    //snatching
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (ready)
            {
                ready = false;

                chanceToSnatch = Random.Range(0f, 1.0f);
                if (chanceToSnatch < 0.7)
                {
                    if (other.gameObject.GetComponent<AI_System>().Score >= 5)
                    {
                        other.gameObject.GetComponent<AI_System>().Score -= 5;
                    }
                    else
                    {
                        other.gameObject.GetComponent<AI_System>().Score = 0;
                    }
                    Score += 5;

                }

                StartCoroutine("CoolDown");
                StartCoroutine("RunAway");
            }
        }
    }


    #region Finding Food and Enemies

    //find all the foods
    public List<GameObject> AllFoods()
    {
        List<GameObject> allFood = new List<GameObject>();

        allFood.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        allFood.AddRange(GameObject.FindGameObjectsWithTag("Chicken"));
        allFood.AddRange(GameObject.FindGameObjectsWithTag("Egg"));
        allFood.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));

        return allFood;
    }

    //find all the enemies
    protected List<GameObject> AllPlayers()
    {
        List<GameObject> allPlayers = new List<GameObject>();
        allPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        allPlayers.Remove(transform.gameObject);
        return allPlayers;
    }

    //which food is the closest
    public Transform FindClosestFood()
    {
        GameObject closestFood = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject food in AllFoods())
        {
            Vector3 diff = food.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestFood = food;
                distance = curDistance;
            }
        }
        Transform ClosestFoodTransform = closestFood.transform;
        smallestDistance1 = distance;

        return ClosestFoodTransform;
    }

    //which enemy is the closest
    protected Transform FindClosestEnemy()
    {
        GameObject closestEnemy = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in AllPlayers())
        {
            //ALEX CHANGE
            if (enemy.gameObject.GetComponent<AI_System>().Score >= 0)
            {
                Vector3 diff = enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closestEnemy = enemy;
                    distance = curDistance;
                }
            }
        }
        Transform ClosestEnemyTransform = closestEnemy.transform;

        smallestDistance2 = distance;

        return ClosestEnemyTransform;
    }

    #endregion


    #region Coroutines 

    //cool down snatching
    protected IEnumerator CoolDown()
    {
        hit.Play();
        yield return new WaitForSecondsRealtime(5f);
        ready = true;
    }

    //avoiding players - used while cool down and in the clone
    protected IEnumerator RunAway()
    {
        navMeshAgent.destination = -ClosestEnemy.transform.position;
        yield return new WaitForSecondsRealtime(3f);
    }

    //producing a clone
    protected IEnumerator CreateClone()
    {
        transform.gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        readyClone = false;
        timeStamp = timer + 30f;

        yield return new WaitForSecondsRealtime(0.5f);

        Instantiate(myPrefab, new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z + 1f), Quaternion.identity, transform);
        player1_scoreText.text = Score.ToString();
        transform.gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }

    #endregion


    //Sjoeke's script
    public void Pause(float Speed)
    {
        navMeshAgent.speed = Speed;
    }

}
