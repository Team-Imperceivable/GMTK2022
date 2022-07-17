using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blunt : Item
{
    public Blunt()
    {
        name = "Blunt";
        effect = "Passive";
        description = "Gives 3 Armor at the start of your turn";
        cost = 0;
        amount = 3;
        diceSides = 0;
        uses = -1;
    }
}
