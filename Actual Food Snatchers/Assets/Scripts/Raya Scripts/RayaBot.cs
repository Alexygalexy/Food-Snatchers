using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayaBot : AI_System, IPauseSystem
{
    protected float smallestDistance1;
    protected float smallestDistance2;
    protected Transform ClosestFood;
    protected Transform ClosestEnemy;

    protected float chanceToSnatch;

    protected bool ready = true, pause = false, readyClone = true;

    //[SerializeField] protected GameObject myPrefab;
    //[SerializeField] protected GameObject myPrefabScoreBoard;

    protected AudioSource hit;
    protected AudioSource collect;
    protected AudioSource[] audioRaya;

    Raya.StateMachine<RayaBot> stateMachine;
    //RayaBaseState currentState;
    Raya.RayaIdleState IdleState = new Raya.RayaIdleState();
    Raya.RayaDefenseState DefenseState = new Raya.RayaDefenseState();
    Raya.RayaAttackState AttackState = new Raya.RayaAttackState();
    Raya.RayaCollectState CollectState = new Raya.RayaCollectState();
    Raya.RayaCloneState CloneState = new Raya.RayaCloneState();

    //State Machine
    protected virtual void Start()
    {
        audioRaya = GetComponents<AudioSource>();
        hit = audioRaya[0];
        collect = audioRaya[1];

        stateMachine = new Raya.StateMachine<RayaBot>(this);

        stateMachine.Start();

        //currentState = IdleState;
        //currentState.EnterState(this);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        //StateMachine
        stateMachine.Update();

        scoreBoard();
        ClosestFood = FindClosestFood();
        ClosestEnemy = FindClosestEnemy();

        if (smallestDistance1 > (smallestDistance2 / 1.5f) && ready == true)   //prioritize enemies: if the difference between the two is less than 1/4 go for the enemy
        {
            movePositionTransform = ClosestEnemy;
            GoToPosition();
            //currentState = CollectState;
            //currentState.EnterState(this);
        }
        else if (!pause)
        {
            //Debug.Log("My next target: " + ClosestEnemy.name);
            movePositionTransform = ClosestFood;
            GoToPosition();
        }

        //if (smallestDistance2 > 20f && readyClone == true)
        //{
        //    Instantiate(myPrefab, transform.position, Quaternion.identity);
        //    readyClone = false;
        //}
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

    //COLLECT
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        collect.Play();
    }

    //ATTACK
    protected override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
            if (ready)
            {
                ready = false;
                Debug.Log("HitHim");

                chanceToSnatch = Random.Range(0f, 1.0f);
                if (chanceToSnatch < 0.7)
                {
                    other.gameObject.GetComponent<AI_System>().Score -= 10;
                    Score += 10;

                    player1_scoreText.text = Score.ToString();

                    StartCoroutine("CoolDown");
                }
            }
            else
            {
                StartCoroutine("CoolDown");
            }
        }
    }

    //---MY FUNCTIONS---
    public List<GameObject> AllFoods()
    {
        List<GameObject> allFood = new List<GameObject>();

        allFood.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        allFood.AddRange(GameObject.FindGameObjectsWithTag("Chicken"));
        allFood.AddRange(GameObject.FindGameObjectsWithTag("Egg"));
        allFood.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));

        return allFood;
    }

    protected List<GameObject> AllPlayers()
    {
        List<GameObject> allPlayers = new List<GameObject>();
        allPlayers.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        allPlayers.Remove(transform.gameObject);
        return allPlayers;
    }

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

    protected Transform FindClosestEnemy()
    {
        GameObject closestEnemy = null;

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in AllPlayers())
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = enemy;
                distance = curDistance;
            }
        }
        Transform ClosestEnemyTransform = closestEnemy.transform;

        smallestDistance2 = distance;

        return ClosestEnemyTransform;
    }

    protected IEnumerator CoolDown()
    {
        pause = true;
        hit.Play();

        navMeshAgent.destination = transform.position;

        yield return new WaitForSecondsRealtime(2f);

        pause = false;
        ready = true;
    }

    //protected void CloneAbility()
    //{
    //    if(smallestDistance2 > 20f)
    //    {
    //        Instantiate(myPrefab, transform.position, Quaternion.identity);
    //    }
    //}

    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }
}
