using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopZoneManager : MonoBehaviour
{
    [Tooltip("Shop zone ambience music")]
    [SerializeField] private AudioSource _Smus;

    public Animator _Animator;

    public bool _canStartRun;

    public PlayerController _playerController;
    public PauseMenuManager _pauseMenuManager;

    public GameObject _TutoText;
    public GameObject _DungText;

    public GameObject _HTPcanvas;
    public GameObject _HTPcontrols;
    public GameObject _HTProoms;

    public GameObject _playerspawn;

    public bool _tutoActive;

    private void Awake()
    {
        _HTPcanvas.SetActive(false);
        LockCursor();
        _Smus.Play();
        _playerController.gameObject.transform.position = _playerspawn.transform.position;
    }

    private void Update()
    {
        ActivateHowToPlay();
        GoToDungeon();
        ShowDoorTexts();
    }

    //Hide cursor
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    //Unhide cursor
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void GoToDungeon()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canStartRun)
        {
            SceneManager.LoadScene(2);
        }
    }

    private void ActivateHowToPlay()
    {
        if (Input.GetKeyDown(KeyCode.R) && _playerController._playerNearTuto)
        {
            _tutoActive = true;
            _pauseMenuManager.GamePaused = true;
            UnlockCursor();
            _HTPcanvas.SetActive(true);
            _HTPcontrols.SetActive(false);
            _HTProoms.SetActive(false);
        }
    }

    private void ShowDoorTexts()
    {
        if (_playerController._playerNearStart) { _DungText.SetActive(true); }
        else { _DungText.SetActive(false); }

        if (_playerController._playerNearTuto && !_tutoActive) { _TutoText.SetActive(true); }
        else { _TutoText.SetActive(false); }
    }

    public void HowToPlayControls()
    {
        _HTPcontrols.SetActive(true);
        _HTProoms.SetActive(false);
    }
    public void HowToPlayRooms()
    {
        _HTPcontrols.SetActive(false);
        _HTProoms.SetActive(true);
    }
    public void HowToPlayExit() {
        LockCursor();
        _tutoActive = false;
        _HTPcanvas.SetActive(false);
        _pauseMenuManager.GamePaused = false;
    }
}
