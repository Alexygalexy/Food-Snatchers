using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using TMPro;


[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject>
{

}
public class AI_System : MonoBehaviour
{
    [Header("Components")]
    protected NavMeshAgent navMeshAgent;

    [Header("AI Move Position")]
    [SerializeField] protected Transform movePositionTransform;

    [Header("Player Score UI")]
    [SerializeField] public TextMeshProUGUI player1_scoreText;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private Transform PlayersLocation;
    [SerializeField] public int Score;

    [Header("Events")]
    public static GameObjectEvent onFoodRemove = new GameObjectEvent();
    protected bool paused;


    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    protected virtual void Update()
    {
        if (!paused)
        {
            GoToPosition();

            scoreBoard();
        }
    }


    protected virtual void OnTriggerEnter(Collider other)
    {

        // Adds a different amount of point depending on the food collected
        // New points are shown on Scoreboard
        // Collected food object gets destroyed
        if (other.gameObject.layer == LayerMask.NameToLayer("Food"))
        {
            switch (other.gameObject.tag)
            {
                case "Apple":
                    Score++;
                    break;

                case "Tomato":
                    Score += 2;
                    break;

                case "Egg":
                    Score += 3;
                    break;

                case "Chicken":
                    Score += 4;
                    break;
            }
            player1_scoreText.text = Score.ToString();
            onFoodRemove.Invoke(other.gameObject);
        }
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("Touching Player");
            //Destroy(other.gameObject);
        }
    }

    #region Reusable Methods

    public virtual void GoToPosition()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }

    protected virtual void scoreBoard()
    {
        // Positions scoreboard above player's head
        ScoreBoard.transform.position = new Vector3(PlayersLocation.transform.position.x, PlayersLocation.transform.position.y + 3, PlayersLocation.transform.position.z);
    }

    #endregion
}
