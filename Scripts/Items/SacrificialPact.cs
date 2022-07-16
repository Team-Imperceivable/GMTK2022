using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificialPact : Item
{
    public SacrificialPact()
    {
        name = "Sacrificial Pact";
        effect = "Pact";
        description = "Exchange health for d6s";
        cost = 2;
        amount = 1;
    }
}

