using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sjoeke_AI : AI_System, IPauseSystem
{
    [SerializeField]
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private Transform FoodTarget;

    private bool pickedRandomFood = false;
    private bool coolingDown = false;

    private float coolDown = 5f;
    private float freezeTime = 2f;
    private float timesUp = 0.5f;

    // The speed for all the players.
    private float speed = 3.5f;

    // List for checking and updating the available foods in the scene. Since my bot is a vegetarian it won't go for chicken.
    List<GameObject> AvailableFood()
    {
        List<GameObject> foods = new List<GameObject>();

        foods.AddRange(GameObject.FindGameObjectsWithTag("Apple"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Tomato"));
        foods.AddRange(GameObject.FindGameObjectsWithTag("Egg"));

        return foods;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    // Start the walking sound, it's in start because when it was in Awake it couldn't find the source of the clip and deactivated this script.
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Walking", true);
    }

    // If AI doen't have a food picked out, pick a new one, then move to the position of the picked food. If there is almost no time left stop playing the walking sound.
    protected override void Update()
    {
        base.Update();

        if (pickedRandomFood == false)
        {
            FoodTarget = RandomFood();
        }
        if (FindObjectOfType<Timer>().timeLeft < timesUp)
        {
            FindObjectOfType<AudioManager>().Play("Walking", false);
        }
            
        movePositionTransform = FoodTarget;
    }

    // If a food has been picked and it hasn't been picked up by someone else, move to the position of the food, if the picked food has been eaten by someone else, stay in current position and pick a new food.
    public override void GoToPosition()
    {
        if (movePositionTransform)
        {
            navMeshAgent.destination = movePositionTransform.position;
        }
        else
        {
            navMeshAgent.destination = transform.position;
            pickedRandomFood = false;
        }
    }

    // If a food has been picked up play the eating sound, play the eating particle and pick a new food.
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        FindObjectOfType<AudioManager>().Play("Eating", true);
        PlayParticles(particles[0]);
        pickedRandomFood = false;
    }

    // When colliding with other players check if the cooldown is not running. If it's not, run the code for freezing enemies.
    protected override void OnCollisionEnter(Collision other)
    {
        if (!coolingDown)
        {
            StartCoroutine(FreezeEnemy(other));
        }
    }

    /// <summary>
    /// Code for freezing enemies. Check if the player that I collided with has the pause code in their AI code, if it exists run the code and set their speed to 0 for 2 seconds. Run the cooldown code so I won't freeze the other player over and over. Play the freezing sound and the particles for hitting. After 2 seconds put the enemy speed back to 3.5f (which is the standard speed for everybody).
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>

    IEnumerator FreezeEnemy(Collision enemy)
    {
        IPauseSystem pause = (IPauseSystem)enemy.transform.GetComponent(typeof(IPauseSystem));
        if (pause != null)
        {
            enemy.gameObject.GetComponent<IPauseSystem>().Pause(0f);
            StartCoroutine(Cooldown());
        }

        FindObjectOfType<AudioManager>().Play("Freezing", true);
        PlayParticles(particles[1]);

        yield return new WaitForSeconds(freezeTime);

        enemy.gameObject.GetComponent<IPauseSystem>().Pause(speed);        
    }

    /// <summary>
    /// Code for the cooldown system for the freeze ability. Cooldown takes 5 seconds, so it's not possible for the AI to use the freeze ability whithin 5 seconds after having used it.
    /// </summary>
    /// <returns></returns>
    IEnumerator Cooldown()
    {
        coolingDown = true;
        yield return new WaitForSeconds(coolDown);
        coolingDown = false;
    }

    /// <summary>
    /// Check the available foods, if there are more than 0 foods available pick a random food from the list, and use it's transform to move there. If there are no foods available use my own transform and stay in the same place.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Code for playing the particle animations. Takes a specific particle animation as a parameter.
    /// </summary>
    /// <param name="particle"></param>

    void PlayParticles(ParticleSystem particle)
    {
        particle.Play();
    }

    /// <summary>
    /// Code that's also in the other bots' code for my special freezing ability. It sets the players speed to the float that has been given as a parameter in the FreezeEnemy coroutine. 
    /// </summary>
    /// <param name="speed"></param>
    public void Pause(float speed)
    {
        navMeshAgent.speed = speed;
    }
}

/// <summary>
/// The interface that is used to access the other bots' code in order to freeze them. 
/// </summary>
public interface IPauseSystem
{
    void Pause(float speed);
}