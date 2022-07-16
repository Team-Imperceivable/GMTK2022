using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterShield : Item
{
    public BetterShield()
    {
        name = "Better Shield";
        effect = "Block";
        description = "I mean, it's an upgrade.";
        cost = 5;
        amount = 6;
    }
}
