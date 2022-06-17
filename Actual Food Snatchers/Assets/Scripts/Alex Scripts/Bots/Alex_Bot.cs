using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    public class Alex_Bot : AI_System, IPauseSystem
    {
        private BotMovementStateMachine movementStateMachine;

        [field: Header("Collisions")]
        [field: SerializeField] public BotLayerData LayerData { get; private set; }

        [field: Header("References")]
        [field: SerializeField] public ParticleSystem smoke;
        [field: SerializeField] public Material invisability_mat;
        [field: SerializeField] public Material original_mat;

        [field: Header("Animation")]
        private Animator anim;


        protected override void Awake()
        {
            movementStateMachine = new BotMovementStateMachine(this);

            movementStateMachine.reusableData.alexMovePoint = movePositionTransform;


            base.Awake();

            movementStateMachine.reusableData.navSpeed = navMeshAgent.speed;

            anim = GetComponentInChildren<Animator>();
        }

        protected void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.MovingState);
        }

        protected override void Update()
        {
            base.Update();

            movePositionTransform = movementStateMachine.reusableData.alexMovePoint;
            navMeshAgent.speed = movementStateMachine.reusableData.navSpeed;

            anim.SetBool("Move", navMeshAgent.velocity.magnitude > 0.01f);

            //Debug.Log(movementStateMachine.reusableData.willSnatch);

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
            //base.OnCollisionEnter(other);

            if (other.gameObject.tag == "Player" && movementStateMachine.reusableData.willSnatch)
            {
                Debug.Log("SNAAAAAAAAAAAAATCH");

                if (other.gameObject.GetComponent<AI_System>().Score >= 5 )
                {
                    this.gameObject.GetComponent<AI_System>().Score += 5;
                    this.gameObject.GetComponent<AI_System>().player1_scoreText.text = Score.ToString();
                    movementStateMachine.reusableData.timeToSnatch = 0;
                    other.gameObject.GetComponent<AI_System>().Score -= 5;
                    other.gameObject.GetComponent<AI_System>().player1_scoreText.text = other.gameObject.GetComponent<AI_System>().Score.ToString();
                    movementStateMachine.reusableData.willSnatch = false;

                    //player1_scoreText.text = Score.ToString();
                    //other.gameObject.GetComponent<AI_System>().player1_scoreText.text = Score.ToString();
                } 
                else
                {
                    this.gameObject.GetComponent<AI_System>().Score += 5;
                    this.gameObject.GetComponent<AI_System>().player1_scoreText.text = Score.ToString();
                    movementStateMachine.reusableData.timeToSnatch = 0;
                    other.gameObject.GetComponent<AI_System>().Score = 0;
                    other.gameObject.GetComponent<AI_System>().player1_scoreText.text = other.gameObject.GetComponent<AI_System>().Score.ToString();
                    movementStateMachine.reusableData.willSnatch = false;
                    //player1_scoreText.text = Score.ToString();
                    //other.gameObject.GetComponent<AI_System>().player1_scoreText.text = Score.ToString();
                }
            }


            //hit.collider.gameObject.GetComponent<AI_System>().Score -= 2;
            //hit.collider.gameObject.GetComponent<AI_System>().player1_scoreText.text = hit.collider.gameObject.GetComponent<AI_System>().Score.ToString();
            //this.gameObject.GetComponent<CleanVitalijBot>().Score += 2;
            //this.gameObject.GetComponent<CleanVitalijBot>().player1_scoreText.text = Score.ToString();


        }

        #region Main Methods
        private void OnDrawGizmos()
        {

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 30f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 5f);


            Collider[] colliders = Physics.OverlapSphere(transform.position, 30f, LayerData.Food);
            Gizmos.color = Color.green;
            for (int i = 0; i < colliders.Length; i++)
            {
                Gizmos.DrawWireSphere(colliders[i].transform.position, 3f);
            }




        }
        #endregion

        #region External Methods

        public void Pause(float Speed)
        {
            navMeshAgent.speed = Speed;
        }

        #endregion
    }
}
