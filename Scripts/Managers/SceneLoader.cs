using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private int currentIndex;
    private AsyncOperation _asyncOperation;
    private bool preloading;

    private void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    public void ChangeToMenu()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Menu"));
    }
}
