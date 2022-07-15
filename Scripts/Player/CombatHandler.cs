using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatHandler : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int shields;
    public int actionPoints;
    [SerializeField] private List<int> modifiers;
    [Header("DICE -SPECIAL SIDED-")]
    [SerializeField] private List<Die> dice;
    [Header("DICE -NORMAL-")]
    [SerializeField] private List<int> numSides;
    private Inventory inventory;

    private int maxActionPoints;
    private bool takingTurn;

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
    }

    private void TakeTurn()
    {
        actionPoints += RollDice();
        if (actionPoints > maxActionPoints)
            actionPoints = maxActionPoints;
        takingTurn = true;
    }

    public void SetTurn(bool turnState)
    {
        takingTurn = turnState;
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
        target.GetComponent<CombatHandler>().TakeDamage(amount);
        Debug.Log(gameObject.name + " dealth " + amount + " damage to " + target.name + "!");
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
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
}
