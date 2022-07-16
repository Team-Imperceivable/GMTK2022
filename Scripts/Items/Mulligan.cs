using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulligan : Item
{
    public Mulligan()
    {
        name = "Mulligan";
        effect = "Dice";
        description = "Once per combat: Reroll your dice. You can only use this at the start of your turn";
        cost = 0;
        amount = 2;
    }
}

