using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> spawnPlaces = new List<GameObject>();
    [SerializeField]
    private List<GameObject> foods = new List<GameObject>();

    private int[] foodIndex = new int[3];
    private int[] tableIndex = new int[3];
    private int maxSpawnPlaces;

    private float tableYOffset = 1.6f;

    private bool GameOn = true;
    private bool SpawnPlacesAvailable = true;


    private void Awake()
    {
        AI_System.onFoodRemove.AddListener(RemoveFood);

        StartCoroutine(foodSpawner(2.5f));
    }


    IEnumerator foodSpawner(float spawnTimer)
    {
        maxSpawnPlaces = spawnPlaces.Count;
        while (GameOn && SpawnPlacesAvailable)
        {
            for (int i = 0; i < foodIndex.Length; i++)
            {
                foodIndex[i] = Random.Range(0, foods.Count);
                tableIndex[i] = Random.Range(0, maxSpawnPlaces);

                GameObject newFood = Instantiate(foods[foodIndex[i]], new Vector3(spawnPlaces[tableIndex[i]].transform.position.x, spawnPlaces[tableIndex[i]].transform.position.y + tableYOffset, spawnPlaces[tableIndex[i]].transform.position.z), Quaternion.identity);
                newFood.transform.parent = spawnPlaces[tableIndex[i]].transform;
                spawnPlaces.RemoveAt(tableIndex[i]);
                maxSpawnPlaces--;
            }            
            if (maxSpawnPlaces < 3)
            {
                SpawnPlacesAvailable = false;
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    private void RemoveFood(GameObject food)
    {
        Destroy(food);
    }


}