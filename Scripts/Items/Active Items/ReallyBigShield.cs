using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigShield : Item
{
    public BigShield()
    {
        name = "Big Shield";
        effect = "Block";
        description = "The sprite is scaled down, but its BIG."; // Sprite does not have to be scaled down
        cost = 18;
        amount = 36;
        diceSides = 0;
        uses = -1;
    }
}
