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

    public bool stunned => skipTurnCounter > 0;
    private string json;
    private int skipTurnCounter;

    private void Start()
    {
        health = maxHealth;
        foreach (int num in numSides)
        {
            dice.Add(new Die(CreateNumList(num)));
        }
        UpdateMaxActionPoints();
        actionPoints = 0;

        inventory = new Inventory();

        inventory.AddItem(new StarterSword());
        inventory.AddItem(new StarterShield());
        LoadInventory();
        skipTurnCounter = 0;
    }

    public void TakeTurn()
    {
        armor = 0;
        if(skipTurnCounter == 0)
        {
            actionPoints += RollDice();
        } else
        {
            skipTurnCounter--;
        }
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

    private void DealDamage(int amount, GameObject target)
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

    private void HealTarget(int amount, GameObject target)
    {
        EnemyCombatHandler enemyCombatHandler = target.GetComponent<EnemyCombatHandler>();
        CombatHandler combatHandler = target.GetComponent<CombatHandler>();
        if(enemyCombatHandler != null)
        {
            enemyCombatHandler.HealAmount(amount);
        } else if(combatHandler != null)
        {
            combatHandler.HealAmount(amount);
        }
    }
    public void HealAmount(int amount)
    {
        health += amount;
        if (maxHealth < health)
            health = maxHealth;
    }
    private void StunTarget(int amount, GameObject target, int cost)
    {
        EnemyCombatHandler enemyCombatHandler = target.GetComponent<EnemyCombatHandler>();
        CombatHandler combatHandler = target.GetComponent<CombatHandler>();
        if (enemyCombatHandler != null)
        {
            if(!enemyCombatHandler.stunned)
            {
                enemyCombatHandler.StunAmount(amount);
                actionPoints -= cost;
            }
        }
        else if (combatHandler != null)
        {
            if (combatHandler.stunned)
            {
                combatHandler.StunAmount(amount);
                
            }
        }
    }
    public void StunAmount(int amount)
    {
        skipTurnCounter += amount;
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
        } else if(item.GetEffect().Equals("Dice"))
        {
            Die diceToRoll = new Die(CreateNumList(item.GetAmount()));
            actionPoints += diceToRoll.Roll();
            actionPoints -= item.GetCost();
        } else if(item.GetEffect().Equals("Pact"))
        {
            Die diceToRoll = new Die(CreateNumList(item.GetAmount()));
            actionPoints += diceToRoll.Roll();
            health -= item.GetCost();
        } else if(item.GetEffect().Equals("Multiply"))
        {
            skipTurnCounter += item.GetCost();
            actionPoints *= item.GetAmount();
        }
        if (target != null)
        {
            if (item.GetEffect().Equals("Damage") && target != gameObject)
            {
                DealDamage(item.GetAmount(), target);
                actionPoints -= item.GetCost();
            } else if(item.GetEffect().Equals("Heal"))
            {
                HealTarget(item.GetAmount(), target);
                actionPoints -= item.GetCost();
            } else if(item.GetEffect().Equals("Stun"))
            {
                StunTarget(item.GetAmount(), target, item.GetCost());
            }
        }
        item.UseItem();
    }

    private List<int> CreateNumList(int num)
    {
        List<int> diceSides = new List<int>();
        for (int i = 1; i <= num; i++)
        {
            diceSides.Add(i);
        }
        return diceSides;
    }
}
