using UnityEngine;


namespace Raya
{
    public class StateMachine<T> 
    {
        State<RayaBot> currentState;
        RayaBot owner; 

        public StateMachine(RayaBot owner) // constructor
        {
            this.owner = owner;
        }

        //RayaIdleState IdleState = new RayaIdleState();
        //RayaDefenseState DefenseState = new RayaDefenseState(); 
        //RayaAttackState AttackState = new RayaAttackState();
        //RayaCollectState CollectState = new RayaCollectState();
        //RayaCloneState CloneState = new RayaCloneState();

        RayaIdleState IdleState = new RayaIdleState();

        //// Start is called before the first frame update
        public void Start()
        {
            StateMachine<RayaBot> stateMachine = new StateMachine<RayaBot>(owner);

            currentState = IdleState;

            currentState.Enter(owner);
        }

        public void Update()
        {
            //Debug.Log("hahah");
            //currentState.Update(owner);
        }
    }
}
