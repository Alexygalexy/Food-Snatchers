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
    private float spawnTimer = 1.5f;

    private bool GameOn = true;
    private bool SpawnPlacesAvailable = true;


    private void Awake()
    {
        AI_System.onFoodRemove.AddListener(RemoveFood);

        // Made by Sjoeke.
        // In AI_System run the TableToList function when a food has been picked up from a table. 
        AI_System.addTable.AddListener(TableToList);

        // Made by Sjoeke.
        // Stat the FoodSpawner coroutine, with as a parameter spawntimer which is 2.5f in this case.
        StartCoroutine(FoodSpawner(spawnTimer));
    }

    /// <summary>
    /// Made by Sjoeke
    /// Coroutine for spawning the food. Set the maximum amount of spawnplaces to the amount of spawnplaces available (available tables to put foods on). While the game is running and while there are spawnplaces available. 3 pieces of food get spawned every time, so the for loop runs 3 times. Pick a random integer that corresponds with the 4 types of food that are available, and pick a random integer that corresponds with the amount of available spawn places. Instantiate this new foodobject, use the food integer to pick the type of food, and use the spawnplace integer to select the table and its Vector3 position where it should spawn. Use the Y offset to place it on top of the table instead of in the middle of the table model which is underneath it.
    /// Set the selected table as a parent of the new food item, and remove the table (spawnplace) from the spawnplaces list. Also subtract one from maxSpawnplaces.
    /// Check if there are less than 3 availble spawnplaces, if true stop the while loop and check the amount of spawnplaces again. 
    /// Repeat 3 times to complete the for loop.
    /// Wait for x amount of seconds and repeat again, if there are 3 or more spawnplaces. 
    /// </summary>
    /// <param name="spawnTimer"></param>
    /// <returns></returns>
    IEnumerator FoodSpawner(float spawnTimer)
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
        CheckSpawnPlaces();
    }

    /// <summary>
    /// Made by Sjoeke
    /// While there are less than 3 available spawn places check when there are more than 2 available again and set the bool to true which exits the while loop, then start the foodspawner coroutine again.
    /// </summary>
    private void CheckSpawnPlaces()
    {
        while (SpawnPlacesAvailable == false)
        {
            if (maxSpawnPlaces > 2)
            {
                SpawnPlacesAvailable = true;
            }
        }
        StartCoroutine(FoodSpawner(spawnTimer));
    }

    /// <summary>
    /// Made by Alex.
    /// </summary>
    /// <param name="food"></param>
    private void RemoveFood(GameObject food)
    {
        Destroy(food);
    }

    /// <summary>
    /// Made by Sjoeke
    /// When someone picked up a food from a table, add the spawnplace to the list and add one to maxSpawnPlaces.
    /// </summary>
    /// <param name="table"></param>
    private void TableToList(GameObject table)
    {
        spawnPlaces.Add(table);
        maxSpawnPlaces++;
    }
}