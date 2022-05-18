using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingObject : MonoBehaviour
{

    public GameObject player;
    public int numberOfFood;

    void OnTriggerEnter(Collider other)
    {
        {
            other.transform.parent = player.transform;
            other.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
            numberOfFood++;
            Debug.Log(numberOfFood);
        }
    }

    void OnCollisionEnter(Collision other) {
        if(numberOfFood >= 1)
        {
            numberOfFood--;
            Debug.Log(numberOfFood);
        }
        if(numberOfFood == 0)
        {
            Debug.Log("Nothing to steal");
            
        }

    }

}
