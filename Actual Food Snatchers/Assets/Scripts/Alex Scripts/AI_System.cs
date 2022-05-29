using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_System : MonoBehaviour
{
    [Header("Components")]
    private NavMeshAgent navMeshAgent;
    private GameObject player;

    [Header("AI Move Position")]
    [SerializeField] private Transform movePositionTransform;

    [Header("AI Gather Food")]
    [SerializeField] private float detectRadius;
    private bool nextFood = true;

    [SerializeField] private List<GameObject> foodList = new List<GameObject>();




    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        StartCoroutine(DetectionRoutine());
    }

    protected virtual void Update()
    {
        GoToPosition();
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
        if (other.tag == "Food")
        {
            nextFood = true;
            foodList.Remove(other.gameObject);
            Destroy(other.gameObject);

        }

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


        foreach (GameObject target in foodList)
        {
            float foodDistance = Vector3.Distance(transform.position, target.transform.position);

            if (foodDistance < detectRadius)
            {
                Debug.Log("Detected Food " + foodList);
                if (nextFood)
                {
                    movePositionTransform.position = target.transform.position;
                    nextFood = false;
                }
            }
        }

    }

    #endregion
}
