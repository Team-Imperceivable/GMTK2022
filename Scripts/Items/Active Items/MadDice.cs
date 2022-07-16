using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadDice : Item
{
    public MadDice()
    {
        name = "Mad Dice";
        effect = "Damage";
        description = "Throw away a random dice at an enemy";
        cost = 1;
        amount = 20;
        diceSides = 0;
        uses = -1;
    }
}

