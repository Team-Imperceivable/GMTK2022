using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialPact : Item
{
    public SacrificialPact()
    {
        name = "Sacrificial Pact";
        effect = "Pact";
        description = "Roll a d6 at the cost of hp";
        cost = 2;
        amount = 1;
        diceSides = 6;
        uses = -1;
    }
}

