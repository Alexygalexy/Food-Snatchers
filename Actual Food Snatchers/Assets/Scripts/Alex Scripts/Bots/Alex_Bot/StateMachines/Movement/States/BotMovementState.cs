using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alex
{
    public class BotMovementState : IState
    {
        protected BotMovementStateMachine stateMachine;

        protected Vector3 centre;
        protected float radius = 30f;

        protected Vector3 foodCentre;
        protected float foodRadius = 3f;

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
        protected virtual Transform FindFood()
        {
            Collider[] colliders = Physics.OverlapSphere(centre, radius, stateMachine.Alex_Bot.LayerData.Food);
            

            Collider nearestCollider = null;
            float minSqrDistance = Mathf.Infinity;
            Transform detectedFood = null;
            bool isEnemy = false;

            for (int i = 0; i < colliders.Length; i++)
            {
                foodCentre = colliders[i].transform.position;
                Collider[] enemies = Physics.OverlapSphere(foodCentre, foodRadius, stateMachine.Alex_Bot.LayerData.Player);
                List<Collider> enemyList = enemies.ToList();
                enemyList.RemoveAll(x => x.transform.root == stateMachine.Alex_Bot.transform);
                enemies = enemyList.ToArray();

                isEnemy = enemies.Length > 0;

                if (!isEnemy)
                {
                    float sqrDistanceToCenter = (centre - colliders[i].transform.position).sqrMagnitude;

                    if (sqrDistanceToCenter < minSqrDistance)
                    {
                        minSqrDistance = sqrDistanceToCenter;
                        nearestCollider = colliders[i];
                        //stateMachine.reusableData.alexMovePoint.position = colliders[i].transform.position;
                        detectedFood = colliders[i].transform;

                        stateMachine.reusableData.foodPos = colliders[i].transform;
                    }
                }
            }
            return detectedFood;
        }

        //protected virtual void EnemyDetect()
        //{
        //    Collider[] colliders = Physics.OverlapSphere(stateMachine.Alex_Bot.transform.position, EnemyDistanceRun, stateMachine.Alex_Bot.LayerData.Player);

        //    Collider nearestCollider = null;
        //    float minSqrDistance = Mathf.Infinity;

        //    for (int i = 0; i < colliders.Length; i++)
        //    {
        //        float sqrDistanceToCentre = (stateMachine.Alex_Bot.transform.position - colliders[i].transform.position).sqrMagnitude;

        //        if (sqrDistanceToCentre < minSqrDistance)
        //        {
        //            minSqrDistance = sqrDistanceToCentre;
        //            nearestCollider = colliders[i];

        //            Vector3 dirToEnemy = stateMachine.Alex_Bot.transform.position - colliders[i].transform.position;

        //            Vector3 newPos = stateMachine.Alex_Bot.transform.position + dirToEnemy;

        //            stateMachine.reusableData.alexMovePoint.position = newPos;
        //        }
        //    }
        //}


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
