using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tom_Bot : AI_System
{
    [Header("Target AI")]
    [SerializeField]
    private List<GameObject> tables = new List<GameObject>();
    [SerializeField]
    private bool GameOn = true;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        movePositionTransform = Go_high_food();
        //CheckBoost();

        /*if(Apple_Prefab != null)
        {
            movePositionTransform.position = new Vector3(-13, -4, -22);
        }*/
    }

    //My AI will not take Apple (except if it's directly on the road, so I don't need to get Apples.
    protected List<GameObject> Targets()
    {
        List<GameObject> target = new List<GameObject>();

        target.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));
        target.AddRange(GameObject.FindGameObjectsWithTag("Egg"));
        target.AddRange(GameObject.FindGameObjectsWithTag("Chicken"));
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
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    ///Function for my AI : All x time, my AI will get a speed boost if my AI has detect a chicken or an egg :)
    /*protected Transform CheckBoost()
    {
        //in progess
    }*/

    //Function to calculate wich food is better to take, the AI will priorise the shortest food beetween : "Chicken", "Egg" and "Tomato",
    //But if a chicken and a Tomato are close, my AI will go to the chicken because Chicken earns 4 points and Eggs 'only' 3.
    //Sometimes it doesn't work, probably because it's my first AI, so sorry in advance, I did my best.
    protected Transform Go_high_food()
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
                ///print("egg");
                best_target = target;
                dist = cur_dist;
            }
            else if (cur_dist < dist && target.name == "Tomato_Prefab(Clone)")
            {
                ///print("tomato");
                best_target = target;
                dist = cur_dist;
            }
            /*else if (cur_dist < dist && food.name == "Test_Bot")
            {
                ///print("tomato");
                best_target = food;
                dist = cur_dist;
            }*/
        }
        Transform best_targetTransorm = best_target.transform;
        return (best_targetTransorm);
    }
}