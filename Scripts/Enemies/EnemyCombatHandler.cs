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

    private void Start()
    {
        health = maxHealth;
    }

    public void RollDamage()
    {
        nextAttack = Random.Range(minDamage, maxDamage + 1);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void AttackPlayer()
    {
        playerCombatHandler.TakeDamage(nextAttack);
    }
}
