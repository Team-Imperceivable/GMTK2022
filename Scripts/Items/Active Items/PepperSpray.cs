using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperSpray : Item
{
    public PepperSpray()
    {
        name = "Pepper Spray";
        effect = "Stun";
        description = "Stun an enemy";
        cost = 15;
        amount = 1;
    }
}
