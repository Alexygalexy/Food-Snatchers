using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sjoeke_AI : AI_System, IPauseSystem
{
    [SerializeField]
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private Transform FoodTarget;
    private Transform currentPosition;
    // private Transform enemyTarget;
    private Vector3 TargetPosition;
    private bool pickedRandomFood = false;
    // private bool closeEnemy = false;

    private int maxDistance = 9;
    // private int maxEnemyDistance = 15;

    private bool coolingDown = false;
    private float coolDown = 5f;
    private float freezeTime = 2f;
    private float speed = 3.5f;

    List<GameObject> players = new List<GameObject>();

    List<GameObject> AvailableFood()
    {
        List<GameObject> foods = new List<GameObject>();

        foods.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Egg"));

        return foods;
    }

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<AudioManager>().Play("Walking");
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }

    protected override void Update()
    {
        base.Update();
        if (pickedRandomFood == false)
        {
            FoodTarget = RandomFood();
            movePositionTransform = FoodTarget;
        }
        /*if (closeEnemy == false)
        {
            enemyTarget = findPlayers();
            movePositionTransform = enemyTarget;
        }*/

    }

    public override void GoToPosition()
    {
        if (movePositionTransform)
        {
            Vector3 minus = movePositionTransform.position - transform.position;
            float distance = minus.sqrMagnitude;
            if (distance > maxDistance)
            {
                navMeshAgent.destination = movePositionTransform.position;
            }
            if (distance < maxDistance)
            {
                pickedRandomFood = false;
            }
            //closeEnemy = false;
        }
        else
        {
            navMeshAgent.destination = transform.position;
            pickedRandomFood = false;
           //closeEnemy = false;
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        FindObjectOfType<AudioManager>().Play("Eating");
        PlayParticles(particles[0]);
    }

    protected override void OnCollisionEnter(Collision other)
    {
        if (!coolingDown)
        {
            StartCoroutine(FreezeEnemy(other));
        }
    }
    
    IEnumerator FreezeEnemy(Collision enemy)
    {
        IPauseSystem pause = (IPauseSystem)enemy.transform.GetComponent(typeof(IPauseSystem));
        if (pause != null)
        {
            enemy.gameObject.GetComponent<IPauseSystem>().Pause(0f);
            StartCoroutine(Cooldown());
        }

        FindObjectOfType<AudioManager>().Play("Freezing");
        PlayParticles(particles[1]);

        yield return new WaitForSeconds(freezeTime);

        enemy.gameObject.GetComponent<IPauseSystem>().Pause(speed);        
    }

    IEnumerator Cooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(coolDown);
        coolingDown = false;
    }

    Transform RandomFood()
    {
        if (AvailableFood().Count > 0 ) 
        {
            int randomFoodInt = Random.Range(0, AvailableFood().Count);

            GameObject randomFood = AvailableFood()[randomFoodInt];

            Transform randomFoodTransform = randomFood.transform;

            pickedRandomFood = true;
            return randomFoodTransform;            
        }
        else
        {
            return transform;
        }
    }

    /*Transform findPlayers()
    {
        GameObject enemy = null;
        foreach (GameObject player in players)
        {
            Vector3 minus = movePositionTransform.position - transform.position;
            float distance = minus.sqrMagnitude;
            if (distance < maxEnemyDistance)
            {
                enemy = player;
                Transform enemyTransform = enemy.transform;
                closeEnemy = true;
                return enemyTransform;
            }
            else
                return null;
        }

    }*/

    void PlayParticles(ParticleSystem particle)
    {
        particle.Play();
    }

    public void Pause(float speed)
    {
        navMeshAgent.speed = speed;
    }
}

public interface IPauseSystem
{
    void Pause(float speed);
}