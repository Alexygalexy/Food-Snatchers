using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class BotEvadeState : BotMovementState
    {
        public BotEvadeState(BotMovementStateMachine botMovementStateMachine) : base(botMovementStateMachine)
        {

        }

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            stateMachine.reusableData.timeToSnatch++;
            Debug.Log(stateMachine.reusableData.timeToSnatch);
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

            if (stateMachine.reusableData.timeToSnatch >= 3)
            {
                stateMachine.ChangeState(stateMachine.SnatchingState);
            }

            if (EnemyDetect() && !stateMachine.reusableData.isInvis)
            {
                EvadeEnemy();
                return;
            }

            stateMachine.ChangeState(stateMachine.MovingState);

        }
        #endregion

        #region Main Methods

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
