using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected string name, effect, description;
    protected int cost;
    protected int amount;
    protected int diceSides;
    protected int uses;
    protected int startAmount, startUses;

    public Item()
    {
        name = "Default Item";
        effect = "Nothing";
        description = "The template item";
        cost = 0;
        amount = 0;
        diceSides = 0;
        uses = -1;
        startAmount = amount;
        startUses = uses;
    }

    public void UseItem()
    {
        if (uses == 0)
            return;
        if (uses > 0)
        {
            uses--;
        }    
    }

    public int GetCost()
    {
        return cost;
    }
    public int GetAmount()
    {
        return amount;
    }
    public string GetEffect()
    {
        return effect;
    }
    public string GetName()
    {
        return name;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetDiceSides()
    {
        return diceSides;
    }
    public int GetUses()
    {
        return uses;
    }
    public void Reset()
    {
        amount = startAmount;
        uses = startUses;
    }
}
