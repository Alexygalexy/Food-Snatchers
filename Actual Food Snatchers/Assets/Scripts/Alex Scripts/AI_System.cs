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
    public TextMeshProUGUI player1_scoreText;
    [SerializeField] private GameObject ScoreBoard;
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

        if (other.gameObject.tag == "Apple")
        {
            Score++;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Banana")
        {
            Score += 2;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Blueberry")
        {
            Score += 3;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Grapes")
        {
            Score += 4;
            player1_scoreText.text = Score.ToString();
        }
        else if (other.gameObject.tag == "Orange")
        {
            Score += 5;
            player1_scoreText.text = Score.ToString();
        }

        Destroy(other.gameObject);
        // if (other.tag == "Food")
        // {
        //     Destroy(other.gameObject);
        // }

    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }

    protected virtual void scoreBoard()
    {
        ScoreBoard.transform.position = new Vector3(PlayersLocation.transform.position.x, PlayersLocation.transform.position.y + 3, PlayersLocation.transform.position.z);
    }

    

    #endregion
}
