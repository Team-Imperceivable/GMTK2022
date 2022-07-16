using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDice : Item
{
    public MagicDice()
    {
        name = "Magic Dice";
        effect = "Passive";
        description = "Add 1 to every dice roll";
        cost = 0;
        amount = 1;
        uses = -1;
        diceSides = 0;
    }
}


