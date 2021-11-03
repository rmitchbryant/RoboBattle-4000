using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script serves as the Sequencer for the behavior tree

public class BTSequence : BTNode
{
    // Set up a list of nodes to use
    private List<BTNode> myNodes = new List<BTNode>();

    public BTSequence(List<BTNode> nodes)
    {
        myNodes = nodes;
    }

    public override BTNodeStates Evaluate()
    {

        bool childRunning = false;

        // Loop through each of the nodes
        foreach (BTNode node in myNodes)
        {
            switch (node.Evaluate())
            {
                // If the node is a failure don't evaluate the next one
                case BTNodeStates.FAILURE:
                    currentNodeState = BTNodeStates.FAILURE;
                    return currentNodeState;

                // If the node reports success then continue
                case BTNodeStates.SUCCESS:
                    continue;

                case BTNodeStates.RUNNING:
                    childRunning = true;
                    continue;

                default:
                    currentNodeState = BTNodeStates.SUCCESS;
                    return currentNodeState;
            }
        }

        // Determine if the node is set to running, if not then report success
        currentNodeState = childRunning ? BTNodeStates.RUNNING : BTNodeStates.SUCCESS;
        return currentNodeState;

    }
}
