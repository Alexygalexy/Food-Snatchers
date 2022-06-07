using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class Alex_Bot : AI_System
    {
        private BotMovementStateMachine movementStateMachine;

        [field: Header("Collisions")]
        [field: SerializeField] public BotLayerData LayerData { get; private set; }


        protected override void Awake()
        {
            movementStateMachine = new BotMovementStateMachine(this);

            movementStateMachine.reusableData.alexMovePoint = movePositionTransform;



            base.Awake();
        }

        protected void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.MovingState);
        }

        protected override void Update()
        {
            base.Update();

            movePositionTransform = movementStateMachine.reusableData.alexMovePoint;

            movementStateMachine.Update();
        }

        protected void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        protected override void OnCollisionEnter(Collision other)
        {
            base.OnCollisionEnter(other);
        }

        #region Main Methods
        private void OnDrawGizmos()
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 30f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 10f);


            Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, LayerData.Food);
            Gizmos.color = Color.green;
            for (int i = 0; i < colliders.Length; i++)
            {
                Gizmos.DrawWireSphere(colliders[i].transform.position, 3f);
            }




        }
        #endregion
    }
}
