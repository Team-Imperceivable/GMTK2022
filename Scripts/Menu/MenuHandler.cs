using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject Options;
    [SerializeField] private GameObject Credits;

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowOptions()
    {
        MainMenu.SetActive(false);
        Options.SetActive(true);

    }

    public void ShowCredits()
    {
        MainMenu.SetActive(false);
        Credits.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        Options.SetActive(false);
        Credits.SetActive(false);
        MainMenu.SetActive(true);
    }
}

