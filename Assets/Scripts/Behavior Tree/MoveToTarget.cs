using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// This node makes the character move to a position in the arena

public class MoveToTarget : BTNode
{

    BehaviorAI myAI;
    Vector3 targetPosition;

    NavMeshAgent agent;
    //Animator animator;
    
    public MoveToTarget(BehaviorAI myAI, NavMeshAgent agent)
    {
        // Assign the BehaviorAI and the NavMeshAgent
        this.myAI = myAI;
        this.agent = agent;
        //this.animator = animator;
    }

    public override BTNodeStates Evaluate()
    {
        // Get the new position to move to
        targetPosition = myAI.GetTargetPosition();

        Debug.Log("Moving");

        // Move to the target position
        agent.SetDestination(targetPosition);
        //animator.SetBool("isWalking", true);

        return BTNodeStates.SUCCESS;
    }
}
