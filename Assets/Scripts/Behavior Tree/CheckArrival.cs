using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This node determines if the enemy has arrived at it's destination

public class CheckArrival : BTNode
{
    BehaviorAI myAI;
    Animator animator;
    float closeness;

    public CheckArrival(BehaviorAI myAI, Animator animator, float closeness)
    {
        // Assign the BehaviorAI
        this.myAI = myAI;
        this.animator = animator;
        this.closeness = closeness;
    }

    public override BTNodeStates Evaluate()
    {
        // Assign the current position and target position
        Vector3 agentPosition = myAI.GetAgentTransform().position;
        Vector3 targetPosition = myAI.GetTargetPosition();

        // Determine the distance between the current position and the target
        float distance = Vector3.Distance(agentPosition, targetPosition);

        // If the agent's current position is close enough to the target mark to go to another one
        if (distance < closeness)
        {
            Debug.Log("Arrived");
            animator.SetBool("isWalking", false);
            return BTNodeStates.SUCCESS;
        }
        else
        {
            Debug.Log("Going");
            animator.SetBool("isWalking", true);
            return BTNodeStates.FAILURE;
        }
    }
}
