using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovementState : IState
{
    protected BotMovementStateMachine stateMachine;

    protected Vector3 centre;
    protected float radius = 30f;

    public BotMovementState(BotMovementStateMachine botMovementStateMachine)
    {
        stateMachine = botMovementStateMachine;
    }

    #region IState Methods
    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);

        centre = stateMachine.Alex_Bot.transform.position;
    }

    public virtual void Exit()
    {
        
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {

    }
    #endregion

    #region Main Methods

    #endregion

    #region Reusable Methods
    protected virtual void FindFood()
    {
        Collider[] colliders = Physics.OverlapSphere(centre, radius, stateMachine.Alex_Bot.LayerData.Food);

        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;

        for (int i = 0; i < colliders.Length; i++)
        {
            float sqrDistanceToCenter = (centre - colliders[i].transform.position).sqrMagnitude;

            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = colliders[i];
                stateMachine.reusableData.alexMovePoint.position = colliders[i].transform.position;
            }
        }
    }
    
    #endregion
}
