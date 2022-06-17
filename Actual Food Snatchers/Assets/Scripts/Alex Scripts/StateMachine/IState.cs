using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    /// <summary>
    /// 
    /// The class that every state machine script root from
    /// 
    /// Creating an interface class to store custom function for the state machine
    /// 
    /// -Alex
    /// 
    /// P.S. - I have been tought by one of my groupmates of this type of statemachine method and have been already using in in the GameLab course.
    /// However my groupmate learned this from this video course: https://www.youtube.com/watch?v=KbA84fHeqXM
    /// 
    /// </summary>
    public interface IState
    {
        void Enter();
        void Exit();
        void Update();
        void PhysicsUpdate();
    }

}