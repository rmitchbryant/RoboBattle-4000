using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This script will control the players combat including:
// Attack rate and which attack is used (ranged or melee)
// Current health as well as player death

public class PlayerCombat : MonoBehaviour
{
    //HealthBar is a separate script that was created for the game on the HealthBar object under HUD
    public HealthBar healthBar;

    public Transform attackPoint;

    public Animator animator;

    public int maxHealth = 100;
    public int currentHealth;

    public float attackRange = 1.0f;
    public int attackDamage = 5;
    public float attackRate = 2.0f;
    public float nextAttackTime = 0f;
    public float nextShootTime = 0f;
    public float rateOfFire = 1.5f;

    public bool playerDead = false;

    GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        // Start with the players health at full
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        // Make sure health does not exceed the maximum
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Make sure melee attacks have an amount of time in between
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(1) && !playerDead)
            {
                animator.Play("Attack_Сold_WP_2");
                MeleeAttack attack = gameObject.GetComponent<MeleeAttack>();
                attack.Attack();
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }

        if(Time.time >= nextShootTime)
        {


            if (Input.GetMouseButton(0) && !playerDead)
            {
                animator.Play("Attack_Rifle");
                RangeAttack range = gameObject.GetComponent<RangeAttack>();
                range.Shoot();
                nextShootTime = Time.time + (1f / rateOfFire);
            }

        }
        // Intentionally damage the player for debugging purposes
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("Ouch!");
            PlayerDamaged(25);
        }

        // Deal heavy damage to the enemy for debugging purposes
        if (Input.GetKeyDown("j"))
        {
            Debug.Log("Doom");
            

            enemy = GameObject.FindGameObjectWithTag("Enemy 1");
            if (enemy == null)
            {
                enemy = GameObject.FindGameObjectWithTag("Enemy 2");

                if (enemy == null)
                {
                    enemy = GameObject.FindGameObjectWithTag("Enemy 3");

                }

            }
            enemy.GetComponent<EnemyController>().TakeDamage(100);
        }

        // Allow the restart of the level
        if (playerDead)
        {
            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

    }

    // Count damage to the player
    public void PlayerDamaged(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (!playerDead)
        {
            animator.Play("GetHit_Rifle");
        }

        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    // The player dies
    void PlayerDie()
    {
        Debug.Log("Player has died!");

        playerDead = true;

        animator.Play("Death_Rifle");

        GetComponent<CharacterController>().enabled = false;
        GetComponent<ThirdPersonMovement>().enabled = false;
        GetComponentInChildren<PlayerLookDirection>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
