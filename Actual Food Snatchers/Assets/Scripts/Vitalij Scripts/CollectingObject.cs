using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingObject : MonoBehaviour
{

    public GameObject player;
    // public GameObject cube;


    // void update()
    // {
    //     // player = GameObject.Find("Player");
    //     // Debug.Log("Found Player!");
    // }
    

    void OnTriggerEnter(Collider other)
    {
        
        {
            Instantiate(other.gameObject, new Vector3(-7, 2, 1), Quaternion.identity);
            Debug.Log("Collected! One point!");
            other.transform.parent = player.transform;
            other.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
            other.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision player)
    {
        // cube = GameObject.Find("Cube");
        // Physics.IgnoreCollision(cube.GetComponent<Collider>(), GetComponent <Collider>());
        
    }
}
