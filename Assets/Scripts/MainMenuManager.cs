using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    //canvas
    [SerializeField] GameObject mainmenucanvas;
    [SerializeField] GameObject settingscanvas;

    [SerializeField] GameObject minaPlay;
    [SerializeField] GameObject minaSettings;
    [SerializeField] GameObject minaExit;
    [SerializeField] GameObject minaReturnMM;

    public AudioSource buttonsoundSource;

    public void Start()
    {
        mainmenucanvas.SetActive(true);
        settingscanvas.SetActive(false);
    }

    public void GoToSettings() {
        buttonsoundSource.Play();
        mainmenucanvas.SetActive(false);
        settingscanvas.SetActive(true);
    }

    public void GoToMainMenu() {
        buttonsoundSource.Play();
        mainmenucanvas.SetActive(true);
        settingscanvas.SetActive(false);
    }

    public void StartGame() {
        buttonsoundSource.Play();
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame() {
        buttonsoundSource.Play();
        Application.Quit();
    }
}
