using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class GameManager : MonoBehaviour
{
    public bool _PlayerOnEntrance;
    public int _MaxRunRooms;
    public bool _roomReset;
    private int _NextRoomId; // 1-60: Combat Room, 61-100: treasure room, shop room -> every 5 rooms (5,10,15,20,25....)
    public bool _roomCleared;

    public GameObject _entranceText;
    public GameObject _Player;
    public GameObject _Enemy;
    public GameObject _Treasure;
    public GameObject _PlayerStartPos;
    public GameObject _EnemyStartPos;
    public GameObject _TreasureStartPos;

    public Animator _GUIanimator;

    public int _TotalEnemiesDefeated;

    private void Awake()
    {
        _Treasure = FindObjectOfType<TreasureS>().gameObject;
        _roomCleared = true;
        _entranceText.SetActive(false);
        _TotalEnemiesDefeated = 0;
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
    }

    private void CreateNextRoom() {
        _NextRoomId = UnityEngine.Random.Range(1, 100);
        if (_NextRoomId >= 1 && _NextRoomId <= 60)
        {
            //combat room
            _Enemy.transform.position = _EnemyStartPos.transform.position;
            _Player.transform.position = _PlayerStartPos.transform.position;

        }
        if (_NextRoomId >= 61 && _NextRoomId <= 100)
        {
            //treasure room
            _Treasure.transform.position = _TreasureStartPos.transform.position;
            _Player.transform.position = _PlayerStartPos.transform.position;
        }
    }

    private void GoToNextRoom() {
        if (Input.GetKeyDown(KeyCode.F) && _roomCleared && _PlayerOnEntrance)
        {
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
}
