using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tom_Bot : AI_System, IPauseSystem
{
    ///[SerializeField]
    ///private bool GameOn = true

    [Header("Speed Boost")]
    [SerializeField]
    private float boostTimer;
    [SerializeField]
    private bool boosting;
    [SerializeField]
    private float period;

    protected AudioSource collect;
    protected AudioSource speedboost;
    protected AudioSource[] audioTom;

    protected virtual void Start()
    {
        audioTom = GetComponents<AudioSource>();
        collect = audioTom[0];
        speedboost = audioTom[1];
        Awake();

    }

    protected override void Awake()
    {
        base.Awake();
        boostTimer = 0;
        period = 0;
        boosting = false;

        audioTom = GetComponents<AudioSource>();
        collect = audioTom[0];
        speedboost = audioTom[1];
    }

    protected override void Update()
    {
        base.Update();
        movePositionTransform = go_high_food();
        speed_boost();

        if (boosting == true)
        {
            boostTimer += UnityEngine.Time.deltaTime;
            if (boostTimer >= 4)
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 4;
                boostTimer = 0;
                period = 0;
                boosting = false;
            }
        }
        /*if(Apple_Prefab != null)
        {
            movePositionTransform.position = new Vector3(-13, -4, -22);
        }*/
    }

    protected List<GameObject> Targets()
    {
        List<GameObject> target = new List<GameObject>();

        target.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));
        target.AddRange(GameObject.FindGameObjectsWithTag("Egg"));
        target.AddRange(GameObject.FindGameObjectsWithTag("Chicken"));
        target.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        target.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        return (target);
    }

    public override void GoToPosition()
    {
        if (movePositionTransform != null)
        {
            navMeshAgent.destination = movePositionTransform.position;
        }
        else if (movePositionTransform == null)
        {
            navMeshAgent.destination = transform.position;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        collect.Play();
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    //Function to calculate wich food is better to take, the AI will priorise the shortest food.
    //But if a chicken and a Tomato are close, my AI will go to the chicken because Chicken earns 4 points and Eggs 'only' 3.
    //Sometimes it doesn't work, probably because it's my first AI, so sorry in advance, I did my best.
    protected Transform go_high_food()
    {
        GameObject best_target = null;

        float dist = Mathf.Infinity;
        /*float value_apple = 1;
        float value_tomato = 2;
        float value_egg = 3;
        float value_chicken = 4;*/

        Vector3 pos = transform.position;

        foreach (GameObject target in Targets())
        {
            Vector3 diff = target.transform.position - pos;
            float cur_dist = diff.sqrMagnitude;
            ///print(target.name);
            if (cur_dist < dist && target.name == "Chicken_Prefab(Clone)")
            {
                ///print("chicken");
                best_target = target;
                dist = cur_dist;
            }
            else if (cur_dist < dist && target.name == "Egg_Prefab(Clone)")
            {
                best_target = target;
                dist = cur_dist;
            }
            else if (cur_dist < dist && target.name == "Tomato_Prefab(Clone)")
            {
                best_target = target;
                dist = cur_dist;
            }
            else if(cur_dist < dist && target.name == "Apple_Prefab(Clone)")
            {
                best_target = target;
                dist = cur_dist;
            }

            ///Trying to go to the shortest player only if no food. But I didn't find how to snatch, or go away after touching so I commented this part.
            /*else if (cur_dist < dist && target.name == "Alex_Bot" || target.name == "Vitalij_Bot" || target.name == "Sjoeke_Bot" || target.name == "Raya_Bot")
            {
                ///print("player");
                best_target = target;
                dist = cur_dist;
            }*/
        }
        Transform best_targetTransorm = best_target.transform;
        return (best_targetTransorm);
    }

    ///Function for my AI : All 10 seconds, my AI will get a speed boost during 4 seconds:) and lost 1 point to balance the game.
    protected void speed_boost()
    {
        if (period > 10)
        {
            boosting = true;
            GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 10;
            speedboost.Play();
            Score -= 1;
            period = 0;
        }
        period += UnityEngine.Time.deltaTime;
    }


    //External Function
    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }
}