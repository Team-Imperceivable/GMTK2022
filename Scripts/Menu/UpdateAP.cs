using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateAP : MonoBehaviour
{
    [SerializeField] private CombatHandler combatHandler;

    private Text actionPointText;

    private void Start()
    {
        actionPointText = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        actionPointText.text = combatHandler.actionPoints.ToString() + "/" + combatHandler.maxActionPoints.ToString();
    }
}
