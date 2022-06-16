using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
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

            GoToFood();

            if (FindToSnatch() && stateMachine.reusableData.timeToSnatch > 1 && stateMachine.reusableData.canInvis)
            {
                stateMachine.ChangeState(stateMachine.SnatchingState);
                return;
            }

            if (!EnemyDetect())
            {
                return;
            }

            if (stateMachine.reusableData.isInvis)
            {
                return;
            }
                
            stateMachine.ChangeState(stateMachine.EvadeState);

        }
        #endregion

        #region Main Methods

        

        #endregion
    }
}
