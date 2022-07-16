using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;

    private bool playerTurn;
    private bool inCombat;

    private FrameInputs inputs;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(!inCombat)
        {
            StartCombat();
        }
        GatherInputs();
        EndPlayerTurn();
    }

    private void EndPlayerTurn()
    {
        if(playerTurn && inputs.endTurn)
        {
            playerController.doneWithTurn = true;
            EnemyTurn();
        }
    }

    private void EnemyTurn()
    {
        enemyController.doneWithTurn = false;
        enemyController.TakeTurn();
        enemyController.UpdateDamage();
        playerTurn = false;
        enemyController.doneWithTurn = true;
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        playerController.TakeTurn();
        playerController.doneWithTurn = false;
        playerTurn = true;
    }

    private void GatherInputs()
    {
        inputs = new FrameInputs
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition),
            select = Input.GetButtonDown("Fire1"),
            endTurn = Input.GetButtonDown("Jump")
        };
    }

    private void StartCombat()
    {
        enemyController.NextEncounter();
        inCombat = true;
        enemyController.UpdateDamage();
        PlayerTurn();
    }
}
