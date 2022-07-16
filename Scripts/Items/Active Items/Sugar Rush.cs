using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRush : Item
{
    public SugarRush()
    {
        name = "Sugar Rush";
        effect = "Multiply";
        description = "Double your points. Skip your next turn.";
        cost = 1;
        amount = 2;
        diceSides = 0;
        uses = -1;
    }
}
