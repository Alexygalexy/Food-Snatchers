using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Alex
{

    /// <summary>
    /// 
    /// Creating the father script of all states that holds reusable methods across the states and allows them to inherit from MovementState
    /// 
    /// -Alex
    /// 
    /// </summary>
    public class BotMovementState : IState
    {
        
        protected BotMovementStateMachine stateMachine;

        protected Vector3 centre;
        protected float radius = 30f;

        protected Vector3 foodCentre;
        protected float foodRadius = 3f;

        protected float EnemyDistanceRun = 5f;

        //Allowing the Statemachine to uses stored variables, like reusableData, LayerData or Alex_Bot
        public BotMovementState(BotMovementStateMachine botMovementStateMachine)
        {
            stateMachine = botMovementStateMachine;
        }

        #region IState Methods
        public virtual void Enter()
        {
            //For testing purposes to see what current state is active
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

        /// <summary>
        /// 
        /// A transform function that creates a scripted collider to check the closest food by using a for loop.
        /// 
        /// -Alex
        /// 
        /// </summary>
        /// <returns> Food Position </returns>
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
                //Another collider created around detected food to ignore the food with enemies close to it.
                Collider[] enemies = Physics.OverlapSphere(foodCentre, foodRadius, stateMachine.Alex_Bot.LayerData.Player);
                // Source: Armando Knowledge
                // Excluding yourself from the collider detection by adding the Array to a list (Because you can only remove objects from a list)
                // and removing the Alex_Bot from it. Aftre we return it back to the array.
                List<Collider> enemyList = enemies.ToList();
                enemyList.RemoveAll(x => x.transform.root == stateMachine.Alex_Bot.transform);
                enemies = enemyList.ToArray();

                // Source: Manno Knowledge
                // Did not know that you can make a boolean like this.
                isEnemy = enemies.Length > 0;

                // If there are no enemies around the food then go to the food
                if (!isEnemy)
                {
                    //Getting the square distance
                    float sqrDistanceToCenter = (centre - colliders[i].transform.position).sqrMagnitude;

                    if (sqrDistanceToCenter < minSqrDistance)
                    {
                        minSqrDistance = sqrDistanceToCenter;
                        nearestCollider = colliders[i];

                        detectedFood = colliders[i].transform;

                        stateMachine.reusableData.foodPos = colliders[i].transform;
                    }
                }
            }
            return detectedFood;
        }

        /// <summary>
        /// 
        /// Create a boolean fuction that checks with a collider that finds closest players and excluding yourself from it.
        /// 
        /// -Alex
        /// </summary>
        /// <returns> boolean </returns>
        protected virtual bool FindToSnatch()
        {
            Collider[] colliders = Physics.OverlapSphere(centre, EnemyDistanceRun, stateMachine.Alex_Bot.LayerData.Player);
            List<Collider> enemyList = colliders.ToList();
            enemyList.RemoveAll(x => x.transform.root == stateMachine.Alex_Bot.transform);
            colliders = enemyList.ToArray();

            //Collider nearestCollider = null;
            //float minSqrDistance = Mathf.Infinity;
            //Vector3 newPos = Vector3.zero;
            bool IsPlayer = colliders.Length > 0;

            if (IsPlayer)
            {
                
                //Activating Invisibility and cooldown for it if true
                if (!stateMachine.reusableData.cdInvis)
                {
                    stateMachine.Alex_Bot.StartCoroutine(InvisabilityDuration());
                    stateMachine.reusableData.isInvis = true;
                    stateMachine.Alex_Bot.StartCoroutine(InvisabilityCooldown());
                    stateMachine.reusableData.cdInvis = true;
                }


                return true;
            }
            else
            {
                return false;
            }
            
            
        }

        /// <summary>
        /// 
        /// Creates a transform of the players position
        /// 
        /// </summary>
        /// <returns> Players Position </returns>
        protected virtual Transform SnatchClosest()
        {
            Collider[] colliders = Physics.OverlapSphere(centre, EnemyDistanceRun, stateMachine.Alex_Bot.LayerData.Player);
            List<Collider> enemyList = colliders.ToList();
            enemyList.RemoveAll(x => x.transform.root == stateMachine.Alex_Bot.transform);
            colliders = enemyList.ToArray();

            Collider nearestCollider = null;
            float minSqrDistance = Mathf.Infinity;
            Transform detectedPlayer = null;
            bool ToSnatch = false;

            ToSnatch = colliders.Length > 0;
            
            
            for (int i = 0; i < colliders.Length; i++)
            {
                float sqrDistanceToCenter = (centre - colliders[i].transform.position).sqrMagnitude;

                if (sqrDistanceToCenter < minSqrDistance)
                {
                    minSqrDistance = sqrDistanceToCenter;
                    nearestCollider = colliders[i];

                    detectedPlayer = colliders[i].transform;
                }
            }
            return detectedPlayer;
        }


        /// <summary>
        /// 
        /// Creates a boolean that changes its state depending if there is a player close to you. 
        /// 
        /// </summary>
        /// <returns> boolean </returns>
        protected virtual bool EnemyDetect()
        {
            Collider[] colliders = Physics.OverlapSphere(centre, EnemyDistanceRun, stateMachine.Alex_Bot.LayerData.Player);
            List<Collider> enemyList = colliders.ToList();
            enemyList.RemoveAll(x => x.transform.root == stateMachine.Alex_Bot.transform);
            colliders = enemyList.ToArray();

            Collider nearestCollider = null;
            float minSqrDistance = Mathf.Infinity;
            Vector3 newPos = Vector3.zero;
            bool IsEnemy = false;

            IsEnemy = colliders.Length > 0;

            if (IsEnemy)
            {
                //Activating Invisibility and cooldow if there is an opponent next to you
                if (!stateMachine.reusableData.cdInvis)
                {
                    stateMachine.Alex_Bot.StartCoroutine(InvisabilityDuration());
                    stateMachine.reusableData.isInvis = true;
                    stateMachine.Alex_Bot.StartCoroutine(InvisabilityCooldown());
                    stateMachine.reusableData.cdInvis = true;
                }
                else if (!stateMachine.reusableData.isInvis)
                {
                    //If the invisibility is inactive then flee from the closest enemy to the opposite direction.
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        float sqrDistanceToCentre = (stateMachine.Alex_Bot.transform.position - colliders[i].transform.position).sqrMagnitude;

                        if (sqrDistanceToCentre < minSqrDistance)
                        {
                            minSqrDistance = sqrDistanceToCentre;
                            nearestCollider = colliders[i];

                            Vector3 dirToEnemy = stateMachine.Alex_Bot.transform.position - colliders[i].transform.position;

                            newPos = stateMachine.Alex_Bot.transform.position + dirToEnemy;

                            stateMachine.reusableData.evadePos = newPos;
                        }
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// A function that is used across the states, which make the players movePoint be equal to the detected food
        /// 
        /// </summary>
        protected virtual void GoToFood()
        {
            stateMachine.reusableData.alexMovePoint.position = FindFood().position;
        }

        /// <summary>
        /// 
        /// Similar to the detected food. Make the players movePoint equal to closest player.
        /// 
        /// </summary>
        protected virtual void SnatchFromPlayer()
        {
            stateMachine.reusableData.alexMovePoint.position = SnatchClosest().position;
        }

        /// <summary>
        /// 
        /// a coroutine for the Ability cooldown
        /// 
        /// </summary>
        /// <returns> cooldown </returns>
        IEnumerator InvisabilityCooldown()
        {
            //An if that checks how many times has the player encountered enemies to make the invis time higher
            if (stateMachine.reusableData.timeToSnatch <= 4)
            {
                stateMachine.reusableData.timeToSnatch++;
                Debug.Log(stateMachine.reusableData.timeToSnatch);
            }
            else
            {
                stateMachine.reusableData.invisTime = 5f;
            }


            yield return new WaitForSeconds(stateMachine.reusableData.invisTime + 3f);
            // setting boolean to make the cooldown be usable again.
            stateMachine.reusableData.cdInvis = false;
            stateMachine.reusableData.canInvis = true;
        }

        /// <summary>
        /// 
        /// a coroutine fo the ability duration
        /// 
        /// </summary>
        /// <returns> duration </returns>
        IEnumerator InvisabilityDuration()
        {
            //plays sfx and particles on start and changes the player look. Additionally makes the player change tags and layers to be un-snatchable by opponents.
            stateMachine.Alex_Bot.smoke.Play();
            stateMachine.Alex_Bot.invisSFX.Play();
            stateMachine.Alex_Bot.gameObject.GetComponentInChildren<Renderer>().material = stateMachine.Alex_Bot.invisability_mat;
            stateMachine.Alex_Bot.gameObject.layer = LayerMask.NameToLayer("Non-Player");
            stateMachine.Alex_Bot.gameObject.tag = "Untagged";
            //Adding ALex_Bot Speed
            stateMachine.reusableData.navSpeed = 4.5f;
            //Making the player be able to snatch only when invisable
            stateMachine.reusableData.willSnatch = true;



            yield return new WaitForSeconds(stateMachine.reusableData.invisTime);
            //returning the player before ablility use
            stateMachine.Alex_Bot.smoke.Stop();
            stateMachine.Alex_Bot.invisSFX.Stop();
            stateMachine.Alex_Bot.gameObject.GetComponentInChildren<Renderer>().material = stateMachine.Alex_Bot.original_mat;
            stateMachine.Alex_Bot.gameObject.layer = LayerMask.NameToLayer("Player");
            stateMachine.Alex_Bot.gameObject.tag = "Player";
            stateMachine.reusableData.navSpeed = 3.5f;
            //boolean to check if invis ended
            stateMachine.reusableData.isInvis = false;
            stateMachine.reusableData.canInvis = false;
        }
        #endregion
    }
}
