using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    /// <summary>
    /// 
    /// State for the player to start snatching from the players
    /// 
    /// -Alex
    /// 
    /// </summary>
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
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Update()
        {
            base.Update();

            //Same check as in the Moving state, but if there are no enemies to be found around.
            if (FindToSnatch() && stateMachine.reusableData.canInvis && stateMachine.reusableData.timeToSnatch > 3)
            {
                //Go to the players position
                SnatchFromPlayer();
                return;
            }

            //Return to moving state if no enemies are around
            stateMachine.ChangeState(stateMachine.MovingState);

        }
        #endregion

        #region Main Methods



        #endregion
    }
}
