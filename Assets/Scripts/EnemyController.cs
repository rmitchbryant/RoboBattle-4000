using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This script will control enemy character damage as well as dying
// It also has the enemy always look in the player's direction
// It also calls on the behavior tree so the enemy can perform certain actions

public class EnemyController : MonoBehaviour, BehaviorAI
{
    public int maxHealth = 20;
    int currentHealth;

    public HealthBar healthBar;
    Transform player;

    public bool isDead = false;

    NavMeshAgent agent;
    Vector3 targetPosition;

    public Animator animator;
    GameObject target = null;
    public SpawnPoint spawnPoint;

    public float rotationSpeed = 5f;

    public string enemyFaction = "PlayerFaction";

    public int die = 1;
    public int dieSides = 10;
    public int thresholdForSuccess = 4;
    public float closenessToPlayer;
    public int chanceToShoot = 10;
    public int thresholdToShoot = 5;
    public float attackRate = 5;
    public float closenessForMelee = 5f;
    public float meleeAttackRate = 5f;

    // Behaviors
    public BTSelector rootAI;
    public BTSequence CheckArrivalSequence;
    public BTSequence MoveSequence;
    public BTSequence DecideToAttack;
    public BTSelector SelectTargetType;
    public BTSelector DecideToMelee;


    // Start is called before the first frame update
    void Start()
    {
        // Set the enemy's max health
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        agent = this.GetComponent<NavMeshAgent>();

        DecideToMelee = new BTSelector(new List<BTNode>
        {
            new EnemyMeleeAttack(this, closenessForMelee, gameObject, meleeAttackRate, animator),
            new FindWander(this),
        });

        DecideToAttack = new BTSequence(new List<BTNode>
        {
            new RandomChanceConditional(die, dieSides, thresholdForSuccess),
            new FindNewTarget(this, enemyFaction, agent),
        });

        SelectTargetType = new BTSelector(new List<BTNode>
        {
            DecideToAttack,
            new FindWander(this),
        });

        // Set up a sequence
        CheckArrivalSequence = new BTSequence(new List<BTNode>
        {
            new CheckArrival(this, animator, closenessToPlayer),
            DecideToMelee,
            SelectTargetType,
        });

        // Set up a sequence
        MoveSequence = new BTSequence(new List<BTNode>
        {
            new MoveToTarget(this, agent),
            new EnemyRangeAttack(this, gameObject, chanceToShoot, thresholdToShoot, attackRate),
        });

        // Set up a selector using the previously set up sequences
        rootAI = new BTSelector(new List<BTNode>
        {
            CheckArrivalSequence,
            MoveSequence,

        });


    }

    public void TakeDamage(int damage)
    {
        // Deal damage to the enemy
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        animator.Play("GetHit_Rifle");
        FindObjectOfType<AudioManager>().Play("EnemyHit");

        // Kill the enemy once health runs out
        if (currentHealth <= 0)
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        Debug.Log("Enemy died.");

        FindObjectOfType<AudioManager>().Play("Death");
        animator.Play("Death_Rifle");

        isDead = true;

        GetComponent<EnemyController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        yield return new WaitForSeconds(2f);


        Destroy(gameObject);

    }

    void FacePlayer()
    {
        // Find the player on the level
        player = GameObject.FindWithTag("PlayerFaction").transform;
        
        // Find the direction of the player and rotate the enemy to always face them
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    // Update is called once per frame
    void Update()
    {

        FacePlayer();
        rootAI.Evaluate();
    }

    public Vector3 SetTargetPosition(Vector3 targetPosition)
    {
        
        this.targetPosition = targetPosition;
        return this.targetPosition;
    }

    public Transform GetAgentTransform()
    {
        return transform;
    }

    public Vector3 GetTargetPosition()
    {
        if (target != null)
        {
            return target.transform.position;
        }
        return targetPosition;
    }

    public GameObject SetTarget(GameObject target)
    {
        this.target = target;
        return target;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
