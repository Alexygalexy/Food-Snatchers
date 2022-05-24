using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectingObject : MonoBehaviour
{

    public GameObject player;
    public int Score;
    bool madePassive = false;

    void Start() {
        
    }
    void OnTriggerEnter(Collider other)
    {
        {
            other.transform.parent = player.transform;
            other.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
            other.gameObject.SetActive(false);
            AddPoints();
            player.gameObject.tag = "Passive";
        }
    }
    void AddPoints()
    {
        Score++;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Stealing")
        {
            GameObject.FindGameObjectWithTag("Stealing").GetComponent<CollectingObject>().Score++;
            this.Score--;
            this.transform.position = new Vector3(transform.position.x-3, transform.position.y-3, transform.position.z);            
        }
        if(Score < 1)
        {
            GameObject.FindGameObjectWithTag("Stealing").GetComponent<Player_Nav_Mesh>().enabled = false;
        }
    }
    

    void makePassive()
    {
        if (!madePassive)
        {
            if (gameObject.tag == "Passive")
            {
                Debug.Log(this.gameObject.name + " is Passive");
                this.GetComponent<Player_Nav_Mesh>().enabled = false;
                this.GetComponent<Player_Nav_Mesh>().enabled = true;
                madePassive = true;
                startChasing();
            }
        }
    }

    void startChasing()
    {
        if (gameObject.tag == "Passive")
        {
            Debug.Log("Start Stealing");
            GameObject.FindGameObjectWithTag("Stealing").GetComponent<Player_Nav_Mesh>().enabled = true;
        }
    }
    void Update()
    {
        makePassive();
    }

}