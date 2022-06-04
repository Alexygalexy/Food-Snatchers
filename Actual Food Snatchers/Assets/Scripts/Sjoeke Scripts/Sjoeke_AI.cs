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
    private List<GameObject> Tables = new List<GameObject>();
    private List<Foods> AvailableFoods = new List<Foods>();

    private int score;

    protected override void Awake()
    {
        //FindFoods();
    }

    protected override void Update()
    {

    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    IEnumerator startFindingFood()
    {
        yield return new WaitForSeconds(1f);
        FindFoods();
    }
    
    private void FindFoods()
    {
        for (int i = 0; i < Tables.Count; i++)
        {
            if (go.transform.childCount > 0)
            {
                Debug.Log("Hello in the first if");/*
                switch (Tables[i].transform.GetChild(0).tag)
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
                Debug.Log(Tables[i].transform.GetChild(0));
                AvailableFoods.Add(new Foods() { FoodName = Tables[i].transform.GetChild(0).name, FoodScore = score, FoodTransform = Tables[i].transform.GetChild(0).transform, FoodPosition = Tables[i].transform.GetChild(0).transform.position});  */              
            }
        }/*
        //Debug.Log(AvailableFoods);
        AvailableFoods.Sort(SortByScore);
        movePositionTransform = AvailableFoods[0].FoodTransform;
        GoToPosition();*/
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
