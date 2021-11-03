using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMeleeAttack : BTNode
{

    BehaviorAI myAI;
    float closeness;
    GameObject enemy;
    float nextAttackTime = 0f;
    float attackRate;
    Animator animator;

    public EnemyMeleeAttack(BehaviorAI myAI, float closeness, GameObject enemy, float attackRate, Animator animator)
    {
        this.myAI = myAI;
        this.closeness = closeness;
        this.enemy = enemy;
        this.attackRate = attackRate;
        this.animator = animator;
    }

    public override BTNodeStates Evaluate()
    {
        Debug.Log("Going for melee");

        Vector3 agentPosition = myAI.GetAgentTransform().position;

        Vector3 player = GameObject.FindGameObjectWithTag("PlayerFaction").transform.position;

        float distance = Vector3.Distance(agentPosition, player);

        if (distance < closeness && Time.time >= nextAttackTime)
        {
            Debug.Log("Got you?");
            animator.Play("Attack_Сold_WP_2");
            enemy.GetComponent<EnemyAttack>().Attack();
            nextAttackTime = Time.time + (1f / attackRate);
            return BTNodeStates.SUCCESS;
        }
        else
        {
            Debug.Log("Nevermind");
            return BTNodeStates.FAILURE;
        }

    }
}
