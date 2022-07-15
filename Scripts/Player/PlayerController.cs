using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CombatHandler combatHandler;
    [SerializeField] private EnemyController enemyController;
    private FrameInputs inputs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GatherInputs();
        CheckSelect();
        CheckEndTurn();
    }

    #region Selecting
    private void CheckSelect()
    {
        if(inputs.select)
        {
            Collider2D[] hitColliders = Physics2D.OverlapPointAll(inputs.mousePos);
            foreach(Collider2D collider in hitColliders)
            {
                
            }
        }
    }
    #endregion

    #region End Turn
    private void CheckEndTurn()
    {
        if(inputs.endTurn)
        {
            combatHandler.SetTurn(false);
            enemyController.TakeTurn();
        }
    }
    #endregion

    private void GatherInputs()
    {
        inputs = new FrameInputs
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition),
            select = Input.GetButtonDown("Fire1"),
            endTurn = Input.GetButtonDown("Jump")
        };
    }
}
