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
    private List<GameObject> AvailableFoods = new List<GameObject>();

    private int score;

    protected override void Awake()
    {
        FindFoods();
    }

    protected override void Update()
    {
        //base.Update();

    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    private void FindFoods()
    {
        AvailableFoods.Add(GameObject.FindGameObjectWithTag("Apple"));
        AvailableFoods.Add(GameObject.FindGameObjectWithTag("Tomato"));
        AvailableFoods.Add(GameObject.FindGameObjectWithTag("Egg"));
        AvailableFoods.Add(GameObject.FindGameObjectWithTag("Chicken"));

        for (int i = 0;  i < AvailableFoods.Count; i++) 
        {
            switch (AvailableFoods[i].gameObject.tag)
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
            //SortedAvailableFoods.Add(new Foods() { FoodName = food.name, FoodScore = score, FoodTransform = food.transform, FoodPosition = food.transform.position});            
        }

        //SortedAvailableFoods.Sort(SortByScore);
        //movePositionTransform = SortedAvailableFoods[0].FoodTransform;
        //GoToPosition();
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
