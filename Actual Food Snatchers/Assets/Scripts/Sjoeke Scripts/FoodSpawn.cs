using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnPlaces = new List<GameObject>();
    [SerializeField]
    private List<GameObject> foods = new List<GameObject>();

    private int[] foodIndex = new int [3];
    private int[] tableIndex = new int[3];
    //private List<int> foodIndex = new List<int>();
    //private List<int> tableIndex = new List<int>();

    private float tableYOffset = 1.6f;

    private bool GameOn = true;

    private int tableIndex1;
    private int tableIndex2;
    private int tableIndex3;

    private int foodIndex1;
    private int foodIndex2;
    private int foodIndex3;


    private void Awake()
    {
        StartCoroutine(foodSpawner(5f));
    }


    IEnumerator foodSpawner(float spawnTimer)
    {
        while (GameOn)
        {
            for (int i = 0; i < foodIndex.Length; i++)
            {
                foodIndex[i] = Random.Range(0, foods.Count);
                tableIndex[i] = Random.Range(0, spawnPlaces.Count);

                if (i == 1)
                {
                    while (tableIndex[i] == tableIndex[i - 1])
                    {
                        tableIndex[i] = Random.Range(0, spawnPlaces.Count);
                    }
                }
                if (i == 2)
                {
                    while (tableIndex[i] == tableIndex[i - 2] || tableIndex[i] == tableIndex[i - 1])
                    {
                        tableIndex[i] = Random.Range(0, spawnPlaces.Count);
                    }
                }

                GameObject newFood = Instantiate(foods[foodIndex[i]], new Vector3(spawnPlaces[tableIndex[i]].transform.position.x, spawnPlaces[tableIndex[i]].transform.position.y + tableYOffset, spawnPlaces[tableIndex[i]].transform.position.z), Quaternion.identity);
                newFood.transform.parent = spawnPlaces[tableIndex[i]].transform;
            }

            yield return new WaitForSeconds(spawnTimer);
        }
    } 

    
}
