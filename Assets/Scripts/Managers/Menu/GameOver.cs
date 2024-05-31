using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public AudioSource _buttonSound;

    public void MainMenu() {
        _buttonSound.Play();
        SceneManager.LoadScene(0);
    }

    public void ExitApp() {
        _buttonSound.Play();
        Application.Quit();
    }
}
