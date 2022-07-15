using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatHandler : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public int maxHealth;
    public int health;
    [SerializeField] private List<int> modifiers;
    [Header("DICE -SPECIAL SIDED-")]
    [SerializeField] private List<Die> dice;
    [Header("DICE -NORMAL-")]
    [SerializeField] private List<int> numSides;


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
    }

    public void DealDamage(GameObject target)
    {
        int amount = RollDice();
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

    public void AttackPlayer()
    {
        DealDamage(player);
    }
}
