using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sjoeke_AI : AI_System, IPauseSystem
{
    protected Transform FoodTarget;
    protected Transform currentPosition;
    protected Vector3 TargetPosition;
    private bool pickedRandomFood = false;

    private int maxDistance = 9;
    bool coolingDown = false;
    float coolDown = 5f;
    float freezeTime = 2f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if (pickedRandomFood == false)
        {
            FoodTarget = RandomFood();
        }

        movePositionTransform = FoodTarget;
        TargetPosition = FoodTarget.position;
    }

    public override void GoToPosition()
    {
        Vector3 minus = TargetPosition - transform.position;
        float distance = minus.sqrMagnitude;
        if (distance > maxDistance)
        {
            navMeshAgent.destination = movePositionTransform.position;
        }
        if (distance < maxDistance)
        {
            pickedRandomFood = false;
        }

    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (!coolingDown)
        {
            StartCoroutine(FreezeEnemy(other));
        }
    }
    
    IEnumerator FreezeEnemy(Collision enemy)
    {
        IPauseSystem pause = (IPauseSystem)enemy.transform.GetComponent(typeof(IPauseSystem));
        if (pause != null)
        {
            enemy.gameObject.GetComponent<IPauseSystem>().Pause(true);
            StartCoroutine(Cooldown());
        }
        //Play hiiiiya sound.

        yield return new WaitForSeconds(freezeTime);

        enemy.gameObject.GetComponent<IPauseSystem>().Pause(false);
        
    }

    IEnumerator Cooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(coolDown);
        coolingDown = false;
    }

    protected List<GameObject> AvailableFood()
    {
        List<GameObject> foods = new List<GameObject>();

        foods.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Egg"));

        return foods;
    }

    protected Transform RandomFood()
    {
        int randomFoodInt = Random.Range(0, AvailableFood().Count);

        GameObject randomFood = AvailableFood()[randomFoodInt];

        Transform randomFoodTransform = randomFood.transform;

        pickedRandomFood = true;
        return randomFoodTransform;
    }

    public void Pause(bool isPaused)
    {
        paused = isPaused;
    }
}

public interface IPauseSystem
{
    void Pause(bool isPaused);
}