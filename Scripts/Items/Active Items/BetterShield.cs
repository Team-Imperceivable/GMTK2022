using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterShield : Item
{
    public BetterShield()
    {
        name = "Better Shield";
        effect = "Block";
        description = "Block 6";
        cost = 5;
        amount = 6;
        diceSides = 0;
        uses = -1;
    }
}
