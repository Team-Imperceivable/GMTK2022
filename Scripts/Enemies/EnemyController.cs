using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool doneWithTurn;

    // Start is called before the first frame update
    void Start()
    {
        doneWithTurn = true;
    }

    public void UpdateDamage()
    {
        BroadcastMessage("RollDamage");
    }

    public void TakeTurn()
    {
        BroadcastMessage("TakeEnemyTurn");   
    }
}
