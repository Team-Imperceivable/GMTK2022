using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;

    public void StartGame()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("SampleScene");
        menuCanvas.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

