using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayaBot : AI_System
{
    protected float smallestDistance1;
    protected float smallestDistance2;
    protected Transform ClosestFood;
    protected Transform ClosestEnemy;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        ClosestFood = FindClosestFood();
        ClosestEnemy = FindClosestEnemy();

        if (smallestDistance1 < (smallestDistance2 / 1.5f))   //prioritize enemies: if the difference between the two is less than 1/4 go for the enemy
        {
            Debug.Log("My next target: " + ClosestFood.name);
            movePositionTransform = ClosestFood;
        }
        else
        {
            Debug.Log("My next target: " + ClosestEnemy.name);
            movePositionTransform = ClosestEnemy;
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

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected List<GameObject> AllFoods()
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

    protected Transform FindClosestFood()
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

}
