using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectingObject : MonoBehaviour
{

    public GameObject player;
    public int Score;
    public TextMeshProUGUI player1_scoreText;
    public TextMeshProUGUI player2_scoreText;
    bool madePassive = false;

    // When triggering other GameObject, this GameObject inherits other GameObject as child
    // This is mostly for visual cue that this GameObject collected other GameObject
    // Adds a point after collecting other GameObject
    // Changes this GameObject's tag, so that this GameObject stop looking for other GameObjects
    void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.tag == "Apple")
        {
            Score++;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Banana")
        {
            Score+=2;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Blueberry")
        {
            Score+=3;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Grapes")
        {
            Score+=4;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Orange")
        {
            Score+=5;
            player1_scoreText.text = Score.ToString();
        }
            Destroy(other.gameObject);
            // player.gameObject.tag = "Passive";
        }
        // if (other.gameObject.tag == "Food")
        // {
            // other.transform.parent = player.transform;
            // other.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z);
            // AddPoints();
        
    // }

    // Raises score by one
    // Writes current player 1's score on canvas 
    // void AddPoints()
    // {
        

    // }

    //When this GameObject collides with other GameObject
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Stealing")
        {
            // Raises the score of stealing's GameObject by one
            // Writes current player 2's score on canvas
            // Reduces this GameObject's score by one
            // Writes current player 1's score on canvas 
            // Moves away this GameObject, so that other GameObject won't collide into it continiously
            GameObject.FindGameObjectWithTag("Stealing").GetComponent<CollectingObject>().Score++;
            player2_scoreText.text = Score.ToString();
            this.Score--;
            player1_scoreText.text = Score.ToString();
            this.transform.position = new Vector3(transform.position.x - 3, transform.position.y - 3, transform.position.z);
        }
        // if (Score < 1)
        // {
        //     // Disables stealing GameObject's NavMesh, so that it won't chases other GameObjects
        //     GameObject.FindGameObjectWithTag("Stealing").GetComponent<Player_Nav_Mesh>().enabled = false;
        // }
    }

    //  Disables and enables this GameObject's NavMesh, which makes it stop looking for other GameObjects
    // void makePassive()
    // {
    //     if (!madePassive)
    //     {
    //         if (gameObject.tag == "Passive")
    //         {

    //             Debug.Log(this.gameObject.name + " is Passive");
    //             this.GetComponent<Player_Nav_Mesh>().enabled = false;
    //             this.GetComponent<Player_Nav_Mesh>().enabled = true;
    //             madePassive = true;
    //             // startChasing();
    //         }
    //     }
    // }
    // Enables stealing GameObject's NavMesh, so that it starts chasing other GameObjects
    // void startChasing()
    // {
    //     if (gameObject.tag == "Passive")
    //     {

    //         Debug.Log("Start Stealing");
    //         GameObject.FindGameObjectWithTag("Stealing").GetComponent<Player_Nav_Mesh>().enabled = true;
    //     }
    // }
    // void Update()
    // {
    //     makePassive();
    // }

}