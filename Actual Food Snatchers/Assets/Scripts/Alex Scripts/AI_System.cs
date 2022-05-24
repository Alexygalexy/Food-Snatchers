using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_System : MonoBehaviour
{
    [Header("Components")]
    private NavMeshAgent navMeshAgent;

    [Header("AI Move Position")]
    [SerializeField] private Transform movePositionTransform;

    protected virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        GoToPosition();
    }

    #region Reusable Methods

    public virtual void GoToPosition()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }

    #endregion
}
