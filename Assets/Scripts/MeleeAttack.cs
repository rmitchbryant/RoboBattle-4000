using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask opponentLayers;
    public GameObject character;
    float attackRange;
    int attackDamage;
    public float damageDelay;


    // Start is called before the first frame update
    void Start()
    {
        attackRange = character.GetComponent<PlayerCombat>().attackRange;
        attackDamage = character.GetComponent<PlayerCombat>().attackDamage;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        StartCoroutine(Hit());
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(damageDelay);

        //Debug.Log("Attack");
        //Debug.Log("Doing " + attackDamage + " damage.");

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, opponentLayers);

        foreach(Collider enemy in hitEnemies)
        {
            //Debug.Log("We hit " + enemy.name);
            if (enemy.GetComponent<EnemyController>() != null)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
            //else if (enemy.GetComponent<Enemy2Controller>() != null)
            //{
            //    enemy.GetComponent<Enemy2Controller>().TakeDamage(attackDamage);
            //}
            //else if (enemy.GetComponent<Enemy3Controller>() != null)
            //{
            //    enemy.GetComponent<Enemy3Controller>().TakeDamage(attackDamage);
            //}
        }
        
    }
}
