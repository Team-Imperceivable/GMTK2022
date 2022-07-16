using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Inventory
{
    public Item[] items;
    public Inventory(Item[] inventoryItems)
    {
        items = new Item[5];
        for(int i = 0; i < inventoryItems.Length; i++)
        {
            items[i] = inventoryItems[i];
        }
    }

    public Inventory()
    {
        items = new Item[5];
    }

    public void AddItem(Item item)
    {
        for(int i = 0; i < 5; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
                return;
            }
        }
    }

    public Item RemoveItem(Item item)
    {
        for(int i = 0; i < 5; i++)
        {
            if (items[i] == item)
            {
                Item temp = items[i];
                items[i] = null;
                return temp;
            }
        }
        return null;
    }
}
