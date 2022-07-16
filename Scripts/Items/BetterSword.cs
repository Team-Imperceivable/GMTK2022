using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterSword : Item
{
    public BetterSword()
    {
        name = "Better Sword";
        effect = "Damage";
        description = "It's an improvement";
        cost = 5;
        amount = 6;
    }
}
