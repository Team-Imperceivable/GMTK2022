using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblingAddict : Item
{
    public GamblingAddict()
    {
        name = "Gambling Addict";
        effect = "Dice";
        description = "Roll a d8";
        cost = 5;
        amount = 8;
    }
}

