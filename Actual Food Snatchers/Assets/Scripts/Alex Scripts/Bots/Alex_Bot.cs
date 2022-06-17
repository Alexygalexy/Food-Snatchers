using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Alex
{
    /// <summary>
    /// 
    /// Alex_Bot is the bot script, which is inheriting from AI_System, which is inheriting from Monobehaviour.
    /// 
    /// -Alex
    /// 
    /// </summary>
    public class Alex_Bot : AI_System, IPauseSystem
    {

        /// <summary>
        /// 
        /// Reference to the Alex_Bot State Machine
        /// 
        /// </summary>
        private BotMovementStateMachine movementStateMachine;

        /// <summary>
        /// Creating a LayeData script, which holds all the Layers that Alex_Bot uses
        /// 
        /// -Alex
        /// </summary>
        [field: Header("Collisions")]
        [field: SerializeField] public BotLayerData LayerData { get; private set; }

        [field: Header("References")]
        [field: SerializeField] public ParticleSystem smoke;
        [field: SerializeField] public Material invisability_mat;
        [field: SerializeField] public Material original_mat;

        [field: Header("Animation")]
        private Animator anim;

        [field: Header("Particles")]
        [field: SerializeField] private List<ParticleSystem> partics = new List<ParticleSystem>();

        [field: Header("SFX")]

        [field: SerializeField] public AudioSource[] sfx;
        private AudioSource foodSFX;
        private AudioSource snatch;
        public AudioSource invisSFX;
        

        protected override void Awake()
        {
            //State machine reference
            movementStateMachine = new BotMovementStateMachine(this);

            //Storing the move position in a specific script that holds all variables and values that are needed for the state machine reference
            movementStateMachine.reusableData.alexMovePoint = movePositionTransform;


            base.Awake();

            movementStateMachine.reusableData.navSpeed = navMeshAgent.speed;

            anim = GetComponentInChildren<Animator>();

            sfx = GetComponents<AudioSource>();
            foodSFX = sfx[0];
            snatch = sfx[1];
            invisSFX = sfx[2];
        }

        protected void Start()
        {
            //Make the Alex_Bot start from Moving State from the start of the game
            movementStateMachine.ChangeState(movementStateMachine.MovingState);
        }

        protected override void Update()
        {
            base.Update();

            //Update the values on Reusable Data script to keep it updated with the state machine
            movePositionTransform = movementStateMachine.reusableData.alexMovePoint;
            navMeshAgent.speed = movementStateMachine.reusableData.navSpeed;


            // Source: https://www.youtube.com/watch?v=wLZPM46zgUo
            // animating the character run animation
            anim.SetBool("Move", navMeshAgent.velocity.magnitude > 0.01f);

            movementStateMachine.Update();
        }

        protected void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }

        /// <summary>
        /// 
        /// On every food gather make the PickUp particle and sfx play
        /// 
        /// -Alex
        /// 
        /// </summary>
        /// <param name="other"></param>
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            partics[0].gameObject.transform.position = other.gameObject.transform.position + new Vector3(0f, 0.5f, 0f);
            partics[0].Play();
            foodSFX.Play();
        }


        /// <summary>
        /// 
        /// On Collision with the player check if other is Player and you are allowed to snatch.
        /// 
        /// Play snatch particles and sfx
        /// 
        /// Subtract the oppenents food by referencing their score and updating their score text. Also an if statement, 
        /// if they are lower than 5 than make it equal to 0, to not make their score negative numbers
        /// 
        /// Refresh the number count of when to snatch with "timeToSnatch" int.
        /// 
        /// -Alex
        /// </summary>
        /// <param name="other"></param>
        protected override void OnCollisionEnter(Collision other)
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && movementStateMachine.reusableData.willSnatch)
            {
                partics[1].gameObject.transform.position = other.gameObject.transform.position + new Vector3(0f, 3f, 0f);
                partics[1].Play();
                snatch.Play();

                if (other.gameObject.GetComponent<AI_System>().Score >= 5 )
                {
                    this.gameObject.GetComponent<AI_System>().Score += 5;
                    this.gameObject.GetComponent<AI_System>().player1_scoreText.text = Score.ToString();
                    movementStateMachine.reusableData.timeToSnatch = 0;
                    other.gameObject.GetComponent<AI_System>().Score -= 5;
                    other.gameObject.GetComponent<AI_System>().player1_scoreText.text = other.gameObject.GetComponent<AI_System>().Score.ToString();
                    movementStateMachine.reusableData.willSnatch = false;
                } 
                else
                {
                    this.gameObject.GetComponent<AI_System>().Score += 5;
                    this.gameObject.GetComponent<AI_System>().player1_scoreText.text = Score.ToString();
                    movementStateMachine.reusableData.timeToSnatch = 0;
                    other.gameObject.GetComponent<AI_System>().Score = 0;
                    other.gameObject.GetComponent<AI_System>().player1_scoreText.text = other.gameObject.GetComponent<AI_System>().Score.ToString();
                    movementStateMachine.reusableData.willSnatch = false;
                }
            }


            //hit.collider.gameObject.GetComponent<AI_System>().Score -= 2;
            //hit.collider.gameObject.GetComponent<AI_System>().player1_scoreText.text = hit.collider.gameObject.GetComponent<AI_System>().Score.ToString();
            //this.gameObject.GetComponent<CleanVitalijBot>().Score += 2;
            //this.gameObject.GetComponent<CleanVitalijBot>().player1_scoreText.text = Score.ToString();


        }

        #region Main Methods

        /// <summary>
        /// 
        /// For development porposes to see the scripted colliders
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// External methods by Sjoeke
        /// 
        /// </summary>
        /// <param name="Speed"></param>
        #region External Methods

        public void Pause(float Speed)
        {
            navMeshAgent.speed = Speed;
        }

        #endregion
    }
}
