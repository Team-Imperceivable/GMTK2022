using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatHandler : MonoBehaviour
{
    public int maxHealth;
    public int health;
    public int armor;
    public int nextAttack;
    [SerializeField] private List<int> modifiers;
    [SerializeField] private int minDamage, maxDamage;
    [SerializeField] private int minMoney, maxMoney;

    public bool stunned => skipTurnCounter > 0;
    private int skipTurnCounter;
    private CombatHandler playerCombatHandler;

    private SFXHandler sfx;

    private void Start()
    {
        health = maxHealth;
        skipTurnCounter = 0;
        sfx = playerCombatHandler.GetComponentInChildren<SFXHandler>();
    }

    public void RollDamage()
    {
        nextAttack = Random.Range(minDamage, maxDamage + 1);
    }
    public void TakeDamage(int amount)
    {
        sfx.PlayEnemyHit();
        health -= amount;
        if(health <= 0)
        {
            ThisDies();
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

    public void TargetPlayer(GameObject player)
    {
        playerCombatHandler = player.GetComponent<CombatHandler>();
        RollDamage();
    }

    public void BuffHealth(int amount)
    {
        maxHealth += amount;
        health += amount;
    }

    public void BuffDamage(int amount)
    {
        nextAttack += Random.Range(0, amount);
        maxDamage += amount;
    }

    public void BuffMoney(int amount)
    {
        maxMoney += amount;
    }

    private void ThisDies()
    {
        //Death Animation Goes Here
        SendMessageUpwards("EnemyDeath", Random.Range(minMoney, maxMoney + 1));
        gameObject.SetActive(false);
    }
}
