using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
        menuCanvas.enabled = true;
    }

    public void ShowOptions()
    {

    }

    public void ShowCredits()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

