using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChanceConditional : BTNode
{
    int dice;
    int sides;
    int threshold;

    public RandomChanceConditional(int dice, int sides, int threshold)
    {
        this.dice = dice;
        this.sides = sides;
        this.threshold = threshold;
    }

    public override BTNodeStates Evaluate()
    {
        int total = 0;

        for (int i = 0; i < dice; i++)
        {
            total += Random.Range(1, (sides +1));
        }

        if (total > threshold)
        {
            return BTNodeStates.SUCCESS;
        }
        else
        {
            return BTNodeStates.FAILURE;
        }
    }
}
