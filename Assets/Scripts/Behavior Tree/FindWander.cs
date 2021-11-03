using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the Node that determines the enemy character's next position to move to

public class FindWander : BTNode
{

    BehaviorAI myAI;

    public FindWander(BehaviorAI myAI)
    {
        // Assign the BehaviorAI
        this.myAI = myAI;
    }

    public override BTNodeStates Evaluate()
    {
        myAI.SetTarget(null);

        // Find a random location on the x and z axes
        float x = Random.Range(-44f, 30f);
        float z = Random.Range(-30f, 40f);

        //Vector3 sphere = Random.insideUnitSphere * 10;

        //float x = sphere.x;
        //float z = sphere.z;

        // Set the enemy character's new destination to the random location
        myAI.SetTargetPosition(new Vector3(x, 0f, z));
        Debug.Log("Finding");

        return BTNodeStates.SUCCESS;
    }
}
