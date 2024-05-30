using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    [Header("Scripts")]
    public PauseMenuManager _pausemanager;
    public GameManager _gamemanager;
    public MonsterScript _Mscript;
    public ShopZoneManager _shopzonemanager;

    [Header("Components")]
    public Animator _Panimator;
    public AudioSource _DashSound;
    public AudioSource _SlashSound;
    public CharacterController _ch;

    [Header("Player variables")]
    //public variables
    public bool _playerNearStart;
    public bool _playerNearTuto;
    public int _playerGold;
    public int _Phealth;
    public bool _dashing;
    public bool _attacking;
    public int _Patkdmg;
    public float _attkCC;
    public float _normalspeed;

    [Header("Camera variables")]
    public GameObject _CamPH;
    public Camera _Cam;

    //private variables
    private float _dashCC = 5f;
    private float _CdashCC;
    private float _dashSpeed = 30f;
    private float _speed;
    private bool _EnemyInRange;
    private float _CattkCC;

    //public int _basePdef;
    //private int _Pdef;
    //public int _PcurrSword;
    //public int _PcurrArmor;
    //public int _basePatkdmg = 10;



    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _speed = _normalspeed;
        _pausemanager = FindObjectOfType<PauseMenuManager>();

        _ch = GetComponent<CharacterController>();
        _CdashCC = 0f;
        _CattkCC = 0f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_CamPH.transform.position, new Vector3(_CamPH.transform.position.x, _CamPH.transform.position.y, _CamPH.transform.position.z + 3));
    }

    private void Update()
    {
        if (_pausemanager.GamePaused == false)
        {
            MovePlayer();
            Attack();
        }
    }

    private void MovePlayer() {

        Vector3 MoveDirection = Quaternion.Euler(0f, _Cam.transform.eulerAngles.y, 0f) * new Vector3(Input.GetAxis("Horizontal"), -3f, Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);

        //Combat Controls
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && _CdashCC == 0f)
        {
            _DashSound.Play();
            _speed = _dashSpeed;
            _CdashCC =+ 1;
            StartCoroutine(DashCooldown());
        }
        
        //final movement
        _ch.Move(MoveDirection * _speed * Time.deltaTime);
        
    }

    private IEnumerator DashCooldown() {
        yield return new WaitForSeconds(0.2f);
        _speed = _normalspeed;
        yield return new WaitForSeconds(_dashCC);
        _CdashCC = 0f;
    }

    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _CattkCC == 0f)
        {
            _SlashSound.Play();
            _CattkCC = +1;
            if (_EnemyInRange && _Mscript != null)
            {
                _Mscript._PlayerHitMe = true;
            }
            StartCoroutine(AttackCooldown());
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _EnemyInRange = true;
        }
        if (other.gameObject.CompareTag("Entrance"))
        {
            _gamemanager._PlayerOnEntrance = true;
        }
        if (other.gameObject.CompareTag("StartWall"))
        {
            _playerNearStart = true;
            _shopzonemanager._canStartRun = true;
        }
        if (other.gameObject.CompareTag("TutoWall"))
        {
            _playerNearTuto = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            _EnemyInRange = false;
        }
        if (other.gameObject.CompareTag("Entrance"))
        {
            _gamemanager._PlayerOnEntrance = false;
        }
        if (other.gameObject.CompareTag("StartWall"))
        {
            _playerNearStart = false;
            _shopzonemanager._canStartRun = false;
        }
        if (other.gameObject.CompareTag("TutoWall"))
        {
            _playerNearTuto = false;
        }
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attkCC);
        _CattkCC = 0f;
    }
}
