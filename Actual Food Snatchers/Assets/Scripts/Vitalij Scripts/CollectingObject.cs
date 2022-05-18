using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingObject : MonoBehaviour
{

    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        {
            other.transform.parent = player.transform;
            other.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
        }
    }

}
