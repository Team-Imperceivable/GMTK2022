using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightedDice : Item
{
    public WeightedDice()
    {
        name = "Weighted Dice";
        effect = "Passive";
        description = "You cannot roll 1s";
        cost = 0;
        amount = 1;
        uses = -1;
        diceSides = 0;
    }
}

