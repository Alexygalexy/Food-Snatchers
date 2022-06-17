using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    /// <summary>
    /// 
    /// State for the player to flee/evade from enemies
    /// 
    /// -ALex
    /// 
    /// </summary>
    public class BotEvadeState : BotMovementState
    {
        public BotEvadeState(BotMovementStateMachine botMovementStateMachine) : base(botMovementStateMachine)
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


            //Make the player perform fleeing if is not invisable and detects enemies.
            if (EnemyDetect() && !stateMachine.reusableData.isInvis)
            {
                //location away from the players
                EvadeEnemy();
                return;
            }

            //Go back to going after food if no enemies are close
            stateMachine.ChangeState(stateMachine.MovingState);

        }
        #endregion

        #region Main Methods

        /// <summary>
        /// 
        /// If not invisable then flee from the players by making the movePosition equal to the evade position
        /// 
        /// -Alex
        /// 
        /// </summary>
        protected void EvadeEnemy()
        {

            if (!stateMachine.reusableData.isInvis)
            {
                stateMachine.reusableData.alexMovePoint.position = stateMachine.reusableData.evadePos;
            }
            
        }

        #endregion
    }
}
