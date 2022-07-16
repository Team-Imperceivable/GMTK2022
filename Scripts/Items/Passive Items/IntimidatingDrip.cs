using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntimidatingDrip : Item
{
    public IntimidatingDrip()
    {
        name = "Intimidating Drip";
        effect = "Passive";
        description = "At the start of your turn, deal damage to all enemies for how many dice you have x3.";
        cost = 0;
        amount = 0;
        uses = -1;
        diceSides = 0;
    }
}

