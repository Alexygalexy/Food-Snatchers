using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class BotSnatchingState : BotMovementState
    {
        public BotSnatchingState(BotMovementStateMachine botMovementStateMachine) : base(botMovementStateMachine)
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

            //stateMachine.reusableData.stopSnatch = false;
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            //SnatchFromPlayer();

            //if (!stateMachine.reusableData.stopSnatch)
            //{
            //    return;
            //}
            if (FindToSnatch() && stateMachine.reusableData.canInvis && stateMachine.reusableData.timeToSnatch > 1)
            {
                SnatchFromPlayer();
                return;
            }

            stateMachine.ChangeState(stateMachine.MovingState);

        }
        #endregion

        #region Main Methods



        #endregion
    }
}