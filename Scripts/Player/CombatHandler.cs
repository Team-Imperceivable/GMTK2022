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
    private int skipTurnCounter;
    private List<int> unrollables;

    private bool canMultiply;
    private bool canReroll;

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
        skipTurnCounter = 0;
        unrollables = new List<int>();
    }

    public void TakeTurn()
    {
        armor = 0;
        PassiveItems();
        canMultiply = true;
        canReroll = true;
        if(skipTurnCounter == 0)
        {
            actionPoints += RollDice();
        } else
        {
            skipTurnCounter--;
        }
        if (actionPoints > maxActionPoints)
            actionPoints = maxActionPoints;
        if (actionPoints < 0)
            actionPoints = 0;
    }

    public void PassiveItems()
    {
        foreach(Item item in inventory.items)
        {
            if(item != null && item.GetEffect().Equals("Passive"))
            {
                if(item.GetName().Equals("Weighted Dice") && !unrollables.Contains(item.GetAmount()))
                {
                    unrollables.Add(item.GetAmount());
                } else if(item.GetName().Equals("Magic Dice") && !modifiers.Contains(item.GetAmount()))
                {
                    modifiers.Add(item.GetAmount());
                } else if(item.GetName().Equals("Intimidating Drip"))
                {
                    GameObject[] enemyArr = GameObject.FindGameObjectsWithTag("Enemy");
                    if(enemyArr.Length > 0)
                    {
                        foreach (GameObject enemy in enemyArr)
                        {
                            DealDamage(dice.Count * item.GetAmount(), enemy);
                        }
                    }
                } else if(item.GetName().Equals("Blunt"))
                {
                    armor += item.GetAmount();
                }
            }
        }
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
            sum += DiceRoll(die);
        }
        foreach (int modifier in modifiers)
        {
            sum += modifier;
        }
        return sum;
    }


    private int DiceRoll(Die die)
    {
        int roll = die.Roll();
        if(unrollables.Contains(roll))
        {
            DiceRoll(die);
        }
        return roll;
    }

    public void UseItem(int itemSlot, GameObject target)
    {
        Item item = inventory.items[itemSlot - 1];
        if (item == null || item.GetCost() > actionPoints || item.GetUses() == 0)
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
        } else if(item.GetEffect().Equals("Multiply") && canMultiply)
        {
            skipTurnCounter += item.GetCost();
            actionPoints *= item.GetAmount();
            canMultiply = false;
        } else if(item.GetEffect().Equals("Reroll") && canReroll)
        {
            if(item.GetName().Equals("Mulligan") && item.GetUses() > 0)
            {
                actionPoints = RollDice();
            } else
            {
                actionPoints = RollDice();
            }
            canReroll = false;
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
            } else if(item.GetEffect().Equals("Throw"))
            {
                DealDamage(item.GetAmount(), target);
                for(int i = 0; i < item.GetCost(); i++)
                {
                    dice.RemoveAt(Random.Range(0, dice.Count));
                }
            }
        }
        if (actionPoints < 0)
            actionPoints = 0;
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

    public void Reset()
    {
        foreach(Item item in inventory.items)
        {
            if(item != null)
            {
                item.Reset();
            }
        }
        armor = 0;
        canMultiply = true;
        canReroll = true;
        skipTurnCounter = 0;
        actionPoints = RollDice();
    }

    public void FullReset()
    {
        Reset();
        int healthIncrease = Random.Range(1, 7);
        maxHealth += healthIncrease;
        health += healthIncrease;
    }

    public bool AddItemToInventory(Item item)
    {
        return inventory.AddItem(item);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void AddDie(Die toBeAdded)
    {
        dice.Add(toBeAdded);
        UpdateMaxActionPoints();
    }

    public void ReplaceItem(int itemSlot, Item item)
    {
        inventory.items[itemSlot] = item;
    }
}
