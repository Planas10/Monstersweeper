using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    //canvas
    public GameObject pausemenucanvas;
    public GameObject pausesettingscanvas;
    public GameObject _HTPcanvas;

    public bool GamePaused;

    public AudioSource buttonsoundSource;

    public void Start()
    {
        pausemenucanvas.SetActive(false);
        pausesettingscanvas.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && (pausemenucanvas.activeSelf == false && pausesettingscanvas.activeSelf == false && _HTPcanvas.activeSelf == false))
        {
            pausemenucanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            GamePaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && (pausemenucanvas.activeSelf == true || pausesettingscanvas.activeSelf == true))
        {
            pausemenucanvas.SetActive(false);
            pausesettingscanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            GamePaused = false;
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        buttonsoundSource.Play();
        pausemenucanvas.SetActive(false);
        pausesettingscanvas.SetActive(false);
        GamePaused = false;
    }

    public void GoToPauseSettings()
    {
        buttonsoundSource.Play();
        pausemenucanvas.SetActive(false);
        pausesettingscanvas.SetActive(true);
    }

    public void GoToPauseMenu()
    {
        buttonsoundSource.Play();
        pausemenucanvas.SetActive(true);
        pausesettingscanvas.SetActive(false);
    }
    public void MainMenu()
    {
        buttonsoundSource.Play();
        GamePaused = false;
        SceneManager.LoadScene("MainMenuScene");
    }
}
