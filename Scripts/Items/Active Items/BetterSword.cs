using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterSword : Item
{
    public BetterSword()
    {
        name = "Better Sword";
        effect = "Damage";
        description = "Deal 6 damage";
        cost = 5;
        amount = 6;
        diceSides = 0;
        uses = -1;
    }
}
