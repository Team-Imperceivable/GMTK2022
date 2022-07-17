using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int encounterNumber;
    public int lastEncounterNumber;
    public bool doneWithTurn;
    [SerializeField] private PlayerController playerController;
    private EncounterManager activeEncounter;

    // Start is called before the first frame update
    void Start()
    {
        doneWithTurn = true;
        encounterNumber = 0;
    }

    public void UpdateDamage()
    {
        BroadcastMessage("RollDamage");
    }

    public void TakeTurn()
    {
        BroadcastMessage("TakeEnemyTurn");   
    }

    public void NextEncounter()
    {
        if(encounterNumber != lastEncounterNumber)
        {
            if(encounterNumber != 0)
            {
                activeEncounter.SetEncounterActive(false);
            }
            encounterNumber++;
            GameObject encounterObj = transform.Find("Encounter " + encounterNumber).gameObject;
            encounterObj.SetActive(true);
            EncounterManager encounter = encounterObj.GetComponent<EncounterManager>();
            activeEncounter = encounter;
            encounter.SetEncounterActive(true);
            activeEncounter.ApplyScaling(encounterNumber);
            activeEncounter.SlideToTarget();
            activeEncounter.GenerateUI();
            playerController.ResetCombat();
        } else
        {
            SceneLoader menuLoader = GameObject.Find("Scene Loader").GetComponent<SceneLoader>();
            menuLoader.ChangeToMenu();
        }
        
    }

    public void GivePlayerMoney(int amount)
    {
        playerController.money += amount;
    }

    public void GivePlayerItem(Item item)
    {
        playerController.AddItem(item);
    }
}
