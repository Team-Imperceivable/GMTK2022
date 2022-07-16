using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whip : Item
{
    public Whip()
    {
        name = "Whip";
        effect = "Damage";
        description = "Damage increases by 2 every time you use";
        cost = 5;
        amount = 1;
        diceSides = 0;
        uses = -1;
    }
    new void UseItem()
    {
        base.UseItem();
        amount += 2;
    }
}
