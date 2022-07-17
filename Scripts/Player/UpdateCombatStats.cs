using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCombatStats : MonoBehaviour
{
    [SerializeField] private CombatHandler combatHandler;
    [SerializeField] private Text healthText;
    [SerializeField] private Text actionPointText;
    [SerializeField] private Text armorText;

    private void Update()
    {
        actionPointText.text = combatHandler.actionPoints.ToString() + "/" + combatHandler.maxActionPoints.ToString();
        armorText.text = combatHandler.armor.ToString();
        healthText.text = combatHandler.health.ToString() + "/" + combatHandler.maxHealth.ToString();
    }
}
