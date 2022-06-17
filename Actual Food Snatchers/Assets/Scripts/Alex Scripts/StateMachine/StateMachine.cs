using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Alex
{

    /// <summary>
    /// 
    /// Creating an abstarct class to install methods. Most importantly a method to change a state "ChangeState"
    /// 
    /// </summary>
    public abstract class StateMachine
    {
        protected IState currentState;

        public void ChangeState(IState newState)
        {
            currentState?.Exit();

            currentState = newState;

            currentState.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void PhysicsUpdate()
        {
            currentState?.PhysicsUpdate();
        }
    }
}