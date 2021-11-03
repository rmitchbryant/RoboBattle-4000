using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : BTNode
{

    BehaviorAI myAI;
    GameObject enemy;
    int chance;
    int threshold;
    float attackRate;
    float nextAttackTime = 0f;


    public EnemyRangeAttack(BehaviorAI myAI, GameObject enemy, int chance, int threshold, float attackRate)
    {
        this.myAI = myAI;
        this.enemy = enemy;
        this.chance = chance;
        this.threshold = threshold;
        this.attackRate = attackRate;
    }

    public override BTNodeStates Evaluate()
    {
        int result = Random.Range(1, chance+1);


        if (result > threshold && Time.time >= nextAttackTime)
        {
            //Debug.Log("Shooting");
            enemy.GetComponent<RangeAttack>().Shoot();
            nextAttackTime = Time.time + (1f / attackRate);
            return BTNodeStates.SUCCESS;
        }
        else
        {
            return BTNodeStates.FAILURE;
        }
    }
}
