using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text armorText;
    [SerializeField] private TMP_Text nextAttackText;
    
    private EnemyCombatHandler enemyCombatHandler;
    private Transform target;
    private Collider2D targetCollider;
    private RectTransform myRect;

    // Start is called before the first frame update
    void Start()
    {
        myRect = gameObject.GetComponent<RectTransform>();
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    public void SetHandler(EnemyCombatHandler handler)
    {
        enemyCombatHandler = handler;
    }

    public void UpdateOffset(Bounds bounds)
    {
        offset.y += bounds.size.y / 2;
    }

    public void UpdatePosition()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        int health = enemyCombatHandler.health;
        lifeText.text = health.ToString();
        armorText.text = enemyCombatHandler.armor.ToString();
        nextAttackText.text = enemyCombatHandler.nextAttack.ToString();
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
