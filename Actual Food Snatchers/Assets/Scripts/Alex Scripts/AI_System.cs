using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AI_System : MonoBehaviour
{
    [Header("Components")]
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    // Drag canvas' text object into player's inspector
    public TextMeshProUGUI player1_scoreText;
    // Drag canvas into player's inspector
    [SerializeField] private GameObject ScoreBoard;
    // Drag player's object into player's inspector to get player's location
    [SerializeField] private GameObject PlayersLocation;
    public int Score;

    [Header("AI Move Position")]
    [SerializeField] private Transform movePositionTransform;

    

    

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        GoToPosition();
        scoreBoard();
    }

    #region Reusable Methods

    public virtual void GoToPosition()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

        // Adds a different amount of point depending on the food collected
        // New points are shown on Scoreboard
        // Collected food object gets destroyed
        switch (other.gameObject.tag)
        {
            case "Apple":
            Score++;
            break;

            case "Banana":
            Score +=2;
            break;

            case "Blueberry":
            Score +=3;
            break;

            case "Grapes":
            Score +=4;
            break;

            case "Orange":
            Score +=5;
            break;
        }
        player1_scoreText.text = Score.ToString();

        Destroy(other.gameObject);
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        // Destroys both player objects when they collide into each other
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }

    protected virtual void scoreBoard()
    {
        // Positions scoreboard above player's head
        ScoreBoard.transform.position = new Vector3(PlayersLocation.transform.position.x, PlayersLocation.transform.position.y + 3, PlayersLocation.transform.position.z);
    }

    

    #endregion
}
