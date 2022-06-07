using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Raya
{
    public class StateMachine<T> 
    {
        State<T> currentState;
        T owner; 

        public StateMachine(T owner) // constructor
        {
            this.owner = owner;
        }

        //RayaIdleState IdleState = new RayaIdleState();
        //RayaDefenseState DefenseState = new RayaDefenseState(); 
        //RayaAttackState AttackState = new RayaAttackState();
        //RayaCollectState CollectState = new RayaCollectState();
        //RayaCloneState CloneState = new RayaCloneState();

        //// Start is called before the first frame update
        //void Start()
        //{
        //    currentState = IdleState;

        //    currentState.EnterState(this);
        //}

        public void Update()
        {
            currentState.Update(owner);
        }
    }
}
