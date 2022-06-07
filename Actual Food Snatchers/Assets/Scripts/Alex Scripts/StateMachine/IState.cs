using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public interface IState
    {
        public void Enter();
        public void Exit();
        public void Update();
        public void PhysicsUpdate();
    }

}