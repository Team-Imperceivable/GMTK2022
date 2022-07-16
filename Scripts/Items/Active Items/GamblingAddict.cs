using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblingAddict : Item
{
    public GamblingAddict()
    {
        name = "Gambling Addict";
        effect = "Dice";
        description = "Roll a 2d8";
        cost = 9;
        amount = 2;
        diceSides = 8;
        uses = -1;
    }
}

