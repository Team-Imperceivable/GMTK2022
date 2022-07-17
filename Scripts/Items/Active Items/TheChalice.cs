using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheChalice : Item
{
    public TheChalice()
    {
        name = "The Chalice";
        effect = "Heal";
        description = "Heal 3hp";
        cost = 6;
        amount = 3;
        diceSides = 0;
        uses = -1;
    }
}
