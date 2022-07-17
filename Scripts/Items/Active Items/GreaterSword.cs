using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterSword : Item
{
    public GreaterSword()
    {
        name = "Greater Sword";
        effect = "Damage";
        description = "Deal 10 damage";
        cost = 7;
        amount = 10;
        diceSides = 0;
        uses = -1;
    }
}
