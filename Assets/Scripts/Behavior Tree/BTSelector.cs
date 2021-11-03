using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script serves as the Behavior Tree Selector

public class BTSelector : BTNode
{
    // Set up a list of Nodes to use
    protected List<BTNode> myNodes = new List<BTNode>();

    public BTSelector(List<BTNode> nodes)
    {
        myNodes = nodes;
    }

    public override BTNodeStates Evaluate()
    {
        // Loop through the list of nodes and evaluate them
        foreach (BTNode node in myNodes)
        {
            switch (node.Evaluate())
            {
                // If the node reports a failure, move on
                case BTNodeStates.FAILURE:
                    continue;

                // If the node reports success then proceed with this node
                case BTNodeStates.SUCCESS:
                    currentNodeState = BTNodeStates.SUCCESS;
                    return currentNodeState;

                default:
                    continue;
            }
        }

        currentNodeState = BTNodeStates.FAILURE;
        return currentNodeState;


    }
}
