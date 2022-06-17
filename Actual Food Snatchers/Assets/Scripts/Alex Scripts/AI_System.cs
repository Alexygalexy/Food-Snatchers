using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using TMPro;

/// <summary>
/// 
/// Initializing the event systeme for food spawning.
/// 
/// -Alex
/// 
/// </summary>
[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject>
{

}
/// <summary>
/// 
/// AI_System works as the base branch for every single Bot in the game, which they refer to
/// 
/// -Alex
/// 
/// </summary>
public class AI_System : MonoBehaviour
{
    [Header("Components")]
    protected NavMeshAgent navMeshAgent;

    [Header("AI Move Position")]
    [SerializeField] protected Transform movePositionTransform;

    [Header("Player Score UI")]
    [SerializeField] public TextMeshProUGUI player1_scoreText;
    [SerializeField] protected GameObject ScoreBoard;
    [SerializeField] private Transform PlayersLocation;
    [SerializeField] public int Score;
    [SerializeField] protected Transform cam;

    [Header("Events")]
    public static GameObjectEvent onFoodRemove = new GameObjectEvent();
    public static GameObjectEvent addTable = new GameObjectEvent();
    protected bool paused;


    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }


    /// <summary>
    /// 
    /// Updating the position of the NavMesh follow point "movePositionTransform" for the Bots to find direction to move
    /// 
    /// -Alex
    /// 
    /// Updating Scoreboard position on top of the player
    /// 
    /// -Vitalij
    /// 
    /// </summary>
    protected virtual void Update()
    {
        GoToPosition();
        scoreBoard();
    }

    /// <summary>
    /// 
    /// 
    /// A trigger enter to check what type of food is being removed and invoking a function in FoodScript
    /// 
    /// -Alex & Vitalij
    /// 
    /// </summary>
    /// <param name="other"></param>
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

            // Made by Sjoeke.
            // When the food has been picked up, run the addTable event and take the parent of the picked up food (which is the table).
            //The addtable event void is run in foodspawn (TableToList).
            addTable.Invoke(other.transform.parent.gameObject);
            onFoodRemove.Invoke(other.gameObject);
        }
    }


    /// <summary>
    /// 
    /// Collision only with players layer
    /// 
    /// -Alex
    /// 
    /// </summary>
    /// <param name="other"></param>
    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("Touching Player");
            //Destroy(other.gameObject);
        }
    }

    #region Reusable Methods


    /// <summary>
    /// Assigning the navigation mesh destination to the move point
    /// 
    /// -Alex
    /// </summary>
    public virtual void GoToPosition()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }


    /// <summary>
    /// 
    /// -Vitalij
    /// 
    /// </summary>
    protected virtual void scoreBoard()
    {
        // Positions scoreboard above player's head
        ScoreBoard.transform.position = new Vector3(PlayersLocation.transform.position.x, PlayersLocation.transform.position.y + 3, PlayersLocation.transform.position.z);

        ScoreBoard.transform.LookAt(cam.position);
        ScoreBoard.transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position);
    }

    #endregion
}
