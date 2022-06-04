using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foods
{
    public string FoodName;
    public int FoodScore;
    public Transform FoodTransform;
    public Vector3 FoodPosition;
}

public class Sjoeke_AI : AI_System
{
    [SerializeField]
    private List<Foods> SortedAvailableFoods = new List<Foods>();
    //private List<GameObject> AvailableFoods = new List<GameObject>();

    private int score;

    [SerializeField]
    private GameObject table;

    protected override void Awake()
    {
        FindFoods();
    }

    protected override void Update()
    {
        base.Update();
        movePositionTransform = FindFoods();
    }

    protected List<GameObject> AvailableFoods()
    {
        List<GameObject> foods = new List<GameObject>();

        foods.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Egg"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Chicken"));

        foreach (GameObject food in foods)
            Debug.Log(food);

        return foods;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
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

    protected Transform FindFoods()
    {
        foreach (GameObject food in AvailableFoods()) 
        {
            Debug.Log("helleo");/*
            switch (food.gameObject.tag)
            {
                case "Apple":
                    score = 1;
                    break;

                case "Tomato":
                    score = 2;
                    break;

                case "Egg":
                    score = 3;
                    break;

                case "Chicken":
                    score = 4;
                    break;                    
            }
            Debug.Log(score);

            SortedAvailableFoods.Add(new Foods() { FoodName = food.name, FoodScore = score, FoodTransform = food.transform, FoodPosition = food.transform.position});            */
        }
        /*
        SortedAvailableFoods.Sort(SortByScore);
        Transform SortedFoodTransform = SortedAvailableFoods[0].FoodTransform;*/

        Transform SortedFoodTransform = table.transform;

        return SortedFoodTransform;
    }

    private int SortByScore (Foods food1, Foods food2)
    {
        return food1.FoodScore.CompareTo(food2.FoodScore);
    }

    #region Main Methods

    

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}
    #endregion
}
