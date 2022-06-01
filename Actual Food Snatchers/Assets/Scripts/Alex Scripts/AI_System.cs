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

    [Header("AI Move Position")]
    [SerializeField] private Transform movePositionTransform;

    [Header("AI Gather Food")]
    [SerializeField] private float detectRadius;
    //private bool nextFood = true;

    //[SerializeField] private List<GameObject> foodList = new List<GameObject>();

    [Header("Player Score UI")]
    [SerializeField] private TextMeshProUGUI player1_scoreText;
    [SerializeField] private GameObject ScoreBoard;
    [SerializeField] private Transform PlayersLocation;
    [SerializeField] private int Score;



    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        //StartCoroutine(DetectionRoutine());
    }

    protected virtual void Update()
    {
        GoToPosition();

        scoreBoard();
    }

    protected virtual IEnumerator DetectionRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FoodDetectionCheck();
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

            //nextFood = true;
            //foodList.Remove(other.gameObject);

            // Adds a different amount of point depending on the food collected
            // New points are shown on Scoreboard
            // Collected food object gets destroyed
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

            Destroy(other.gameObject);

        

    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Destroy(other.gameObject);
        }
    }

    #region Reusable Methods

    public virtual void GoToPosition()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    protected virtual void FoodDetectionCheck()
    {
        //Collider[] rangeChecks = Physics.OverlapSphere(transform.position, detectRadius, foodMask);

        //foreach (Collider target in rangeChecks)
        //{
        //    Debug.Log("Detected Food " + rangeChecks);
        //    Transform transport_target = target.transform;
        //    if (nextFood)
        //    {
        //        movePositionTransform.position = transport_target.position;
        //        nextFood = false;
        //    }

        //}


        //foreach (GameObject target in foodList)
        //{
        //    float foodDistance = Vector3.Distance(transform.position, target.transform.position);

        //    if (foodDistance < detectRadius)
        //    {
        //        Debug.Log("Detected Food " + foodList);
        //        if (nextFood)
        //        {
        //            movePositionTransform.position = target.transform.position;
        //            nextFood = false;
        //        }
        //    }
        //}

    }

    protected virtual void scoreBoard()
    {
        // Positions scoreboard above player's head
        ScoreBoard.transform.position = new Vector3(PlayersLocation.transform.position.x, PlayersLocation.transform.position.y + 3, PlayersLocation.transform.position.z);
    }

    #endregion
}
