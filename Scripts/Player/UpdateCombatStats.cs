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
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private RectTransform hpBackground;
    public Vector2 sizeDelta;

    private Vector2 anchorPos;

    private void Start()
    {
        anchorPos = hpBar.anchoredPosition;
    }

    private void Update()
    {
        actionPointText.text = combatHandler.actionPoints.ToString() + "/" + combatHandler.maxActionPoints.ToString();
        armorText.text = combatHandler.armor.ToString();
        healthText.text = combatHandler.health.ToString() + "/" + combatHandler.maxHealth.ToString();

        float percentHealth = (float) combatHandler.health / (float) combatHandler.maxHealth;
        float newX = hpBackground.rect.width * percentHealth;
        hpBar.sizeDelta = new Vector2(newX, 100f);
        Vector2 newPos = anchorPos;
        newPos.x -= (hpBackground.rect.width - newX) / 2f;
        hpBar.anchoredPosition = newPos;
    }
}
