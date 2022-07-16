using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterSword : Item
{
    public StarterSword()
    {
        name = "Starter Sword";
        effect = "Damage";
        description = "Does this even qualify as a sword?";
        cost = 3;
        amount = 2;
    }
}
