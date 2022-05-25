using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnPlaces = new List<GameObject>();
    [SerializeField]
    private List<GameObject> foods = new List<GameObject>();

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
            foodIndex1 = Random.Range(0, foods.Count);
            foodIndex2 = Random.Range(0, foods.Count);
            foodIndex3 = Random.Range(0, foods.Count);

            tableIndex1 = Random.Range(0, spawnPlaces.Count);
            tableIndex2 = Random.Range(0, spawnPlaces.Count);
            tableIndex3 = Random.Range(0, spawnPlaces.Count);

            while (tableIndex2 == tableIndex1)
            {
                tableIndex2 = Random.Range(0, spawnPlaces.Count);
            }
            while (tableIndex3 == tableIndex1 || tableIndex3 == tableIndex2)
            {
                tableIndex3 = Random.Range(0, spawnPlaces.Count);
            }

            Instantiate(foods[foodIndex1], new Vector3(spawnPlaces[tableIndex1].transform.position.x, spawnPlaces[tableIndex1].transform.position.y + tableYOffset, spawnPlaces[tableIndex1].transform.position.z), Quaternion.identity);
            Instantiate(foods[foodIndex2], new Vector3(spawnPlaces[tableIndex2].transform.position.x, spawnPlaces[tableIndex2].transform.position.y + tableYOffset, spawnPlaces[tableIndex2].transform.position.z), Quaternion.identity);
            Instantiate(foods[foodIndex3], new Vector3(spawnPlaces[tableIndex3].transform.position.x, spawnPlaces[tableIndex3].transform.position.y + tableYOffset, spawnPlaces[tableIndex3].transform.position.z), Quaternion.identity);

            yield return new WaitForSeconds(spawnTimer);
        }
    } 

    
}
