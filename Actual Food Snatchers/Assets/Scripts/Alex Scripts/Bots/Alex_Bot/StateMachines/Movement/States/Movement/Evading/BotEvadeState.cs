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


        }
        #endregion

        #region Main Methods

        protected void EvadeEnemy()
        {

        }

        #endregion
    }
}
