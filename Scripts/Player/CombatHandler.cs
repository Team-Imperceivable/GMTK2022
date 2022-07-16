using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int armor;
    public int actionPoints;
    [SerializeField] private List<int> modifiers;
    [Header("DICE -SPECIAL SIDED-")]
    [SerializeField] private List<Die> dice;
    [Header("DICE -NORMAL-")]
    [SerializeField] private List<int> numSides;

    private Inventory inventory;
    public int maxActionPoints;

    private string json;

    private void Start()
    {
        health = maxHealth;
        foreach (int num in numSides)
        {
            List<int> diceSides = new List<int>();
            for (int i = 1; i <= num; i++)
            {
                diceSides.Add(i);
            }
            dice.Add(new Die(diceSides));
        }
        UpdateMaxActionPoints();
        actionPoints = 0;

        inventory = new Inventory();

        inventory.AddItem(new StarterSword());
        inventory.AddItem(new StarterShield());
        LoadInventory();
    }

    public void TakeTurn()
    {
        armor = 0;
        actionPoints += RollDice();
        if (actionPoints > maxActionPoints)
            actionPoints = maxActionPoints;
    }

    private void UpdateMaxActionPoints()
    {
        maxActionPoints = 0;
        foreach(Die die in dice)
        {
            maxActionPoints += die.MaxRoll();
        }
        maxActionPoints *= 2;
    }

    public void DealDamage(int amount, GameObject target)
    {
        target.GetComponent<EnemyCombatHandler>().TakeDamage(amount);
        Debug.Log(gameObject.name + " dealt " + amount + " damage to " + target.name + "!");
    }
    public void TakeDamage(int amount)
    {
        int combined = armor + health;
        combined -= amount;
        if(combined < health)
        {
            health = combined;
        }
    }

    private int RollDice()
    {
        int sum = 0;
        foreach (Die die in dice)
        {
            sum += die.Roll();
        }
        foreach (int modifier in modifiers)
        {
            sum += modifier;
        }
        return sum;
    }

    public void SaveInventory()
    {
        json = JsonUtility.ToJson(inventory);
    }

    public void LoadInventory()
    {
        JsonUtility.FromJsonOverwrite(json, inventory);
    }

    public void UseItem(int itemSlot, GameObject target)
    {
        Item item = inventory.items[itemSlot - 1];
        if (item == null || item.GetCost() > actionPoints)
            return;
        Debug.Log(item.GetName());
        
        if (item.GetEffect().Equals("Block"))
        {
            armor += item.GetAmount();
            actionPoints -= item.GetCost();
        }
        if (target != null)
        {
            if (item.GetEffect().Equals("Damage"))
            {
                DealDamage(item.GetAmount(), target);
                actionPoints -= item.GetCost();
            }
        }
    }
}
