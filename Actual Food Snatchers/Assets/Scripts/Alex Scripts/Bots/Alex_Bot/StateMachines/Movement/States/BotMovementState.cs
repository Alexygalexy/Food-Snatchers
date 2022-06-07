using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class BotMovementState : IState
    {
        protected BotMovementStateMachine stateMachine;

        protected Vector3 centre;
        protected float radius = 30f;

        protected float EnemyDistanceRun = 10f;

        public BotMovementState(BotMovementStateMachine botMovementStateMachine)
        {
            stateMachine = botMovementStateMachine;
        }

        #region IState Methods
        public virtual void Enter()
        {
            Debug.Log("State: " + GetType().Name);


        }

        public virtual void Exit()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void Update()
        {
            centre = stateMachine.Alex_Bot.transform.position;
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
                    Debug.DrawRay(centre, colliders[i].transform.position, Color.green);
                }

            }
        }

        protected virtual void EnemyDetect()
        {
            Collider[] colliders = Physics.OverlapSphere(stateMachine.Alex_Bot.transform.position, EnemyDistanceRun, stateMachine.Alex_Bot.LayerData.Player);

            Collider nearestCollider = null;
            float minSqrDistance = Mathf.Infinity;

            for (int i = 0; i < colliders.Length; i++)
            {
                float sqrDistanceToCentre = (stateMachine.Alex_Bot.transform.position - colliders[i].transform.position).sqrMagnitude;

                if (sqrDistanceToCentre < minSqrDistance)
                {
                    minSqrDistance = sqrDistanceToCentre;
                    nearestCollider = colliders[i];

                    Vector3 dirToEnemy = stateMachine.Alex_Bot.transform.position - colliders[i].transform.position;

                    Vector3 newPos = stateMachine.Alex_Bot.transform.position + dirToEnemy;

                    stateMachine.reusableData.alexMovePoint.position = newPos;
                }
            }
        }

        //protected virtual Transform FindFood(Transform[] food)
        //{
        //    Transform tMin = null;
        //    float minDist = Mathf.Infinity;
        //    Vector3 currentPos = stateMachine.Alex_Bot.transform.position;
        //    foreach (Transform t in food)
        //    {
        //        float dist = Vector3.Distance(t.position, currentPos);
        //        if (dist < minDist)
        //        {
        //            tMin = t;
        //            minDist = dist;
        //        }
        //    }
        //    return tMin;
        //}

        #endregion
    }
}
