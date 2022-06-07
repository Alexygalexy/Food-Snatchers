using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void PhysicsUpdate();
    }

}