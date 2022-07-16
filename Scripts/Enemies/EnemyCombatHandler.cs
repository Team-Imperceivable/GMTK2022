using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatHandler : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int nextAttack;
    [SerializeField] private List<int> modifiers;
    [SerializeField] private int minDamage, maxDamage;
    [SerializeField] private CombatHandler playerCombatHandler;

    public bool stunned => skipTurnCounter > 0;
    private int skipTurnCounter;

    private void Start()
    {
        health = maxHealth;
        skipTurnCounter = 0;
    }

    public void RollDamage()
    {
        nextAttack = Random.Range(minDamage, maxDamage + 1);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            SendMessageUpwards("NextEncounter");
        }
    }
    public void HealAmount(int amount)
    {
        health += amount;
        if (maxHealth < health)
            health = maxHealth;
    }

    private void AttackPlayer()
    {
        playerCombatHandler.TakeDamage(nextAttack);
    }

    public void TakeEnemyTurn()
    {
        if(skipTurnCounter == 0)
        {
            AttackPlayer();
        } else
        {
            skipTurnCounter--;
        }
        
    }

    public void StunAmount(int amount)
    {
        skipTurnCounter += amount;
    }
}
