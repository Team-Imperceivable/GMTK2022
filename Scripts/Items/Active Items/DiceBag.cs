using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceBag : Item
{
    public DiceBag()
    {
        name = "Dice Bag";
        effect = "Dice";
        description = "Gain a temperary d6 for this combat";
        cost = 6;
        amount = 1;
        diceSides = 6;
        uses = -1;
    }
}

