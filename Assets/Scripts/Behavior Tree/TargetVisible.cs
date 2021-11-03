using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetVisible : BTNode
{

    BehaviorAI myAI;


    public TargetVisible(BehaviorAI myAI)
    {
        this.myAI = myAI;
    }

    public override BTNodeStates Evaluate()
    {

        if (myAI.GetTarget() == null)
        {
            return BTNodeStates.FAILURE;
        }

        RaycastHit hit;

        if (Physics.Raycast(myAI.GetTransform().position, myAI.GetTransform().forward, out hit, 1000f))
        {

            if(hit.collider.transform.root.gameObject == myAI.GetTarget())
            {
                return BTNodeStates.SUCCESS;
            }

        }
        else
        {
            return BTNodeStates.FAILURE;
        }

        return BTNodeStates.FAILURE;
    }
}
