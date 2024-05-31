using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController _playerController;

    public bool _PlayerOnEntrance;
    public int _MaxRunRooms;
    public bool _roomReset;
    private int _NextRoomId; // 1-50: Combat Room, 51-70: heal room, 71-100 treasure room
    public bool _roomCleared;

    public GameObject _Player;
    public GameObject _PlayerStartPos;
    public GameObject _Enemy;
    public GameObject _EnemyStartPos;
    public GameObject _EnemyStandByPos;
    public GameObject _Treasure;
    public GameObject _TreasureStartPos;
    public GameObject _TreasureStandByPos;
    public GameObject _TreasureText;
    public GameObject _Mstone;
    public GameObject _MstoneStartPos;
    public GameObject _MstoneStandByPos;
    public GameObject _MstoneText;
    public GameObject _entranceText;

    public Animator _GUIanimator;

    public int _totalRooms;

    public int _TotalEnemiesDefeated;

    private void Awake()
    {
        _totalRooms = 0;
        _Treasure = FindObjectOfType<TreasureS>().gameObject;
        _playerController = FindObjectOfType<PlayerController>().gameObject.GetComponent<PlayerController>();
        _roomCleared = true;
        _entranceText.SetActive(false);
        _TotalEnemiesDefeated = 0;
    }

    private void Start()
    {
        _MstoneText.SetActive(false);
        _TreasureText.SetActive(false);
        StartRun();
    }

    private void Update()
    {
        if (_PlayerOnEntrance)
        {
            _entranceText.SetActive(true);
        }
        else
        {
            _entranceText.SetActive(false);
        }
        GoToNextRoom();
        ActivateDeactivateTexts();
    }

    private void CreateNextRoom()
    {
        _NextRoomId = UnityEngine.Random.Range(1, 100);
        if (_NextRoomId >= 1 && _NextRoomId <= 2) //combat room
        {
            //combat room
            _Enemy.transform.position = _EnemyStartPos.transform.position;
            _Treasure.transform.position = _TreasureStandByPos.transform.position;
            _Mstone.transform.position = _MstoneStandByPos.transform.position;
            _Player.transform.position = _PlayerStartPos.transform.position;
            _roomCleared = false;

        }
        if (_NextRoomId >= 3 && _NextRoomId <= 4) //heal room
        {
            //combat room
            _Enemy.transform.position = _EnemyStandByPos.transform.position;
            _Treasure.transform.position = _TreasureStandByPos.transform.position;
            _Mstone.transform.position = _MstoneStartPos.transform.position;
            _Player.transform.position = _PlayerStartPos.transform.position;
            _roomCleared = true;

        }
        if (_NextRoomId >= 5 && _NextRoomId <= 100) //chest room
        {
            //treasure room
            _Treasure.transform.position = _TreasureStartPos.transform.position;
            _Mstone.transform.position = _MstoneStandByPos.transform.position;
            _Enemy.transform.position = _EnemyStandByPos.transform.position;
            _Player.transform.position = _PlayerStartPos.transform.position;
            _roomCleared = true;
        }
        Debug.Log(_NextRoomId);
    }

    private void StartRun() {
        CreateNextRoom();
        Invoke("FadeIn", 1);
        _roomCleared = false;
        _PlayerOnEntrance = false;
    }

    private void GoToNextRoom() {
        if (Input.GetKeyDown(KeyCode.F) && _roomCleared && _PlayerOnEntrance)
        {
            _totalRooms++;
            FadeOut();
            CreateNextRoom();
            Invoke("FadeIn", 1);
            _roomCleared = false;
            _PlayerOnEntrance = false;
        }
    }

    public void FadeOut() {
        _GUIanimator.Play("FadeOut");
    }
    public void FadeIn() {
        _GUIanimator.Play("FadeIn");
    }

    private void ActivateDeactivateTexts() {
        if (_playerController._canHeal) { _MstoneText.SetActive(true); }
        else { _MstoneText.SetActive(false); }

        if (_playerController._canOpenChest) { _TreasureText.SetActive(true); }
        else { _TreasureText.SetActive(false); }
    }
}
