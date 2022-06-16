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
    [SerializeField] private float range = 5;

    [SerializeField]protected float angleSpeed = 45.0f;
    private bool collided;
    private bool steal;
    float timer = 5.0f;
    //public GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    









    protected override void Awake()
    {
        // base.Awake();
        
        steal = true;
        collided = true;
    }

    protected void Start() {
        //Debug.Log(players);
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
        
    }

    protected override void Update()
    {

        base.scoreBoard();
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if (dist < 1f)
        {
            IncreaseIndex();
        }
        Patrol();
        

        if(!steal && !collided)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                steal=true;
                collided=true;
                
            }
        }
    }
    private void FixedUpdate() {
        if(steal){
            DetectEnemy();
        }
    }


    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        // base.OnCollisionEnter(other);
        // if (other.gameObject.tag == "Player")
        // {
        //     collided=true;
        // }
        if (other.gameObject.tag == ("Player"))
        {
            if(collided)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(0f, 0f, 15f, ForceMode.Impulse);
            }
        }
    }

    protected virtual void Patrol()
    {
        Vector3 targetDir = waypoints[waypointIndex].position - transform.position;
        targetDir.y = 0.0f;
        float step = angleSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), step);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    protected virtual void IncreaseIndex()
    {
        
        waypointIndex++;
        if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }

    public virtual void DetectEnemy()
    {
        Vector3 detectEnemy = Vector3.forward;
        Ray theRay = new Ray(transform.position, transform.TransformDirection(detectEnemy * range));
        Debug.DrawRay(transform.position, transform.TransformDirection(detectEnemy * range));

        if (Physics.Raycast(theRay, out RaycastHit hit, range))
        {
            if (hit.collider.tag == "Player")
            {
               if(collided)
               {
                   Debug.Log("STOLEN");                   
                   hit.collider.gameObject.GetComponent<AI_System>().Score -=5;
                   hit.collider.gameObject.GetComponent<AI_System>().player1_scoreText.text = Score.ToString();
                   this.gameObject.GetComponent<Vitalij_Bot>().Score += 5;
                   this.player1_scoreText.text = Score.ToString();
                   
                   timer = 5.0f;
                   steal=false;
                   collided=false;
                   
               }
            }
        }
    }

    

    #region Main Methods

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);
    //}
    #endregion
}
