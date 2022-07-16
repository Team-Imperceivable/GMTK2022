using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    protected string name, effect, description;
    protected int cost;
    protected int amount;

    public Item()
    {
        name = "Default Item";
        effect = "Nothing";
        description = "The template item";
        cost = 0;
        amount = 0;
    }

    public void UseItem()
    {

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
}
