using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private CombatHandler combatHandler;
    private void LoadNextScene()
    {
        combatHandler.SaveInventory();
    }
    private void NextScene()
    {

    }
}
