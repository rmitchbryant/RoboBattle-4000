using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FindNewTarget : BTNode
{

    BehaviorAI myAI;
    string enemyFaction;
    NavMeshAgent agent;

    public FindNewTarget(BehaviorAI myAI, string enemyFaction, NavMeshAgent agent)
    {
        this.myAI = myAI;
        this.enemyFaction = enemyFaction;
        this.agent = agent;
    }

    public override BTNodeStates Evaluate()
    {
        
        GameObject target = GameObject.FindGameObjectWithTag(enemyFaction);

        if (target != null)
        {

            //int randomChoice = Random.Range(0, targets.Length);
            //Debug.Log("Going in");
            
            myAI.SetTarget(target);

            return BTNodeStates.SUCCESS;
        }
        else
        {
            return BTNodeStates.FAILURE;
        }
    }
}
