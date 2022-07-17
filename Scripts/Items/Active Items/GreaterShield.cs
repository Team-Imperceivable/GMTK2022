using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreaterShield : Item
{
    public GreaterShield()
    {
        name = "Greater Shield";
        effect = "Block";
        description = "Block 10";
        cost = 7;
        amount = 10;
        diceSides = 0;
        uses = -1;
    }
}
