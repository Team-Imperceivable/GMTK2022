using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFollowTarget : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text armorText;

    private EnemyCombatHandler enemyCombatHandler;
    private Transform target;
    private Collider2D targetCollider;

    // Start is called before the first frame update
    void Start()
    {
        
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

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position) + offset;
        lifeText.text = enemyCombatHandler.health.ToString() + "/" + enemyCombatHandler.maxHealth.ToString();
        armorText.text = enemyCombatHandler.armor.ToString();
    }
}
