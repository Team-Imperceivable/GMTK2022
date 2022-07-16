using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingGem : Item
{
    public HealingGem()
    {
        name = "Healing Gem";
        effect = "Heal";
        description = "Oooo shiny";
        cost = 4;
        amount = 1;
    }
}
