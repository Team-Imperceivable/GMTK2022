using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterSword : Item
{
    public StarterSword()
    {
        name = "Starter Sword";
        effect = "Damage";
        description = "Deal 2 damage";
        cost = 3;
        amount = 2;
        diceSides = 0;
        uses = -1;
    }
}
