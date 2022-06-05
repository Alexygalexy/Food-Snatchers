using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alex_Bot : AI_System
{
    private BotMovementStateMachine movementStateMachine;

    [field: Header("Collisions")]
    [field: SerializeField] public BotLayerData LayerData { get; private set; }

    protected override void Awake()
    {
        movementStateMachine = new BotMovementStateMachine(this);

        base.Awake();
    }

    protected void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.MovingState);
    }

    protected override void Update()
    {
        base.Update();

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

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}
    #endregion
}
