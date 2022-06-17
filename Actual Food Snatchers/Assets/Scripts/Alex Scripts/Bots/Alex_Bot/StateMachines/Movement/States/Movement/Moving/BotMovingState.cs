using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    /// <summary>
    /// 
    /// Bot Moving to food state
    /// 
    /// -Alex
    /// 
    /// </summary>
    public class BotMovingState : BotMovementState
    {
        public BotMovingState(BotMovementStateMachine botMovementStateMachine) : base(botMovementStateMachine)
        {

        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            //Make the player go to the food
            GoToFood();

            // An if statement to check if the player is going to snatch or evade from the enemy
            if (FindToSnatch() && stateMachine.reusableData.timeToSnatch > 3 && stateMachine.reusableData.canInvis)
            {
                //changing state to snatching if counter is higher, detected an enemy and is invisable
                stateMachine.ChangeState(stateMachine.SnatchingState);
                return;
            }

            //If the player doesn't encounter enemise perform the GoToFood
            if (!EnemyDetect())
            {
                return;
            }

            //Make the player still go after food if encounter and enemy and is invisable
            if (stateMachine.reusableData.isInvis)
            {
                return;
            }
            
            //Change state to evade state if the if's are not met
            stateMachine.ChangeState(stateMachine.EvadeState);

        }
        #endregion

        #region Main Methods

        

        #endregion
    }
}
