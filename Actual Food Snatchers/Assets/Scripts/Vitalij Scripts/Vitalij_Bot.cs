using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Vitalij_Bot : AI_System
{
    public Transform[] waypoints;
    public int speed;

    private int waypointIndex;
    private float dist;
    [SerializeField] private float range;


   

    protected override void Awake()
    {
        // base.Awake();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
    }

    protected override void Update()
    {
        base.scoreBoard();
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if(dist < 1f)
        {
            IncreaseIndex();
        }
        Patrol();
        
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        base.OnCollisionEnter(other);
    }

    protected virtual void Patrol ()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected virtual void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);

        
       
    }

    #region Main Methods

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}
    #endregion
}
