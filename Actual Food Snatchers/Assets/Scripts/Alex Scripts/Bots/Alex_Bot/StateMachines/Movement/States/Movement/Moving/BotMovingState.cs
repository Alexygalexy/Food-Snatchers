using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        FindFood();
    }
    #endregion

    #region Main Methods

    #endregion
}