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
    public CameraOrbit _cameraOrbit;

    [Header("Components")]
    public Animator _Panimator;
    public AudioSource _DashSound;
    public AudioSource _SlashSound;
    public AudioSource _Steps;
    public AudioSource _playerHit;
    public CharacterController _ch;

    [Header("Player variables")]
    //public variables
    public bool _playerNearStart;
    public bool _playerNearTuto;
    public int _playerGold;
    public int _Patkdmg;
    public float _attkCC;
    public float _dashCC;
    public float _normalspeed;
    public bool _canHeal;
    public bool _canOpenChest;
    public bool _CanMove;
    public bool _damaged;
    public bool _running;
    public bool _attacking;
    public bool _dashing;
    public bool _enemyHasHit;

    public float _PMaxHealth;
    public float _PCurrHealth;
    public float _MissHealth;

    [Header("Camera variables")]
    public GameObject _CamPH;
    public Camera _Cam;

    //private variables
    private float _CdashCC;
    private float _dashSpeed = 30f;
    private float _speed;
    private bool _EnemyInRange;
    private float _CattkCC;
    private bool Hactive;
    private bool Vactive;

    //public int _basePdef;
    //private int _Pdef;
    //public int _PcurrSword;
    //public int _PcurrArmor;
    //public int _basePatkdmg = 10;



    private void Awake()
    {
        _PCurrHealth = _PMaxHealth;

        _CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        _speed = _normalspeed;
        _pausemanager = FindObjectOfType<PauseMenuManager>();

        _ch = GetComponent<CharacterController>();
        _CdashCC = 0f;
        _CattkCC = 0f;
    }

    private void Update()
    {
        Debug.Log(_PCurrHealth);
        _MissHealth = _PMaxHealth - _PCurrHealth;
        if (_pausemanager.GamePaused == false && _CanMove)
        {
            MovePlayer();
            Attack();
        }
        HealthManager();
    }

    private void MovePlayer() {

        Vector3 MoveDirection = Quaternion.Euler(0f, _Cam.transform.eulerAngles.y, 0f) * new Vector3(Input.GetAxis("Horizontal"), -3f, Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);

        //Combat Controls
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && _CdashCC == 0f)
        {
            _dashing = true;
            _DashSound.Play();
            _speed = _dashSpeed;
            _CdashCC =+ 1;
            StartCoroutine(DashCooldown());
        }
        
        //final movement
        _ch.Move(MoveDirection * _speed * Time.deltaTime);

        if (Input.GetButtonDown("Horizontal"))
        {
            Hactive = true;
            _Steps.Play();
        }

        if (Input.GetButtonDown("Vertical"))
        {
            Vactive = true;
            _Steps.Play();
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            Hactive = false;

            if (Vactive == false)
            {
                _Steps.Pause();
            }
        }

        if (Input.GetButtonUp("Vertical"))
        {
            Vactive = false;

            if (Hactive == false)
            {
                _Steps.Pause();
            }
        }

        if (Math.Abs(MoveDirection.x) > 0.1f || Math.Abs(MoveDirection.z) > 0.1f)
        {
            _running = true;
        }
        else
        {
            _running = false;
        }

    }

    private void HealthManager() {
        if (_enemyHasHit)
        {
            _playerHit.Play();
            _PCurrHealth = _PCurrHealth - _Mscript._MmeleeDmg;
        }
    }

    private IEnumerator DashCooldown() {
        yield return new WaitForSeconds(0.2f);
        _speed = _normalspeed;
        _dashing = false;
        yield return new WaitForSeconds(_dashCC);
        _CdashCC = 0f;
    }

    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _CattkCC == 0f && !_dashing)
        {
            _CanMove = false;
            _attacking = true;
            _SlashSound.Play();
            _CattkCC = +1;
            if (_EnemyInRange && _Mscript != null)
            {
                _Mscript._PlayerHitMe = true;
            }
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attkCC);
        _attacking = false;
        _CanMove = true;
        _CattkCC = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) { _EnemyInRange = true; }
        if (other.gameObject.CompareTag("Entrance")) { _gamemanager._PlayerOnEntrance = true; }
        if (other.gameObject.CompareTag("StartWall")) { _playerNearStart = true; _shopzonemanager._canStartRun = true; }
        if (other.gameObject.CompareTag("TutoWall")) { _playerNearTuto = true; }
        if (other.gameObject.CompareTag("Mstone")) { _canHeal = true; Debug.Log("patatudo"); }
        //if (other.gameObject.CompareTag("Chest")) { _canOpenChest = true; }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy")) { _EnemyInRange = false; }
        if (other.gameObject.CompareTag("Entrance")) { _gamemanager._PlayerOnEntrance = false; }
        if (other.gameObject.CompareTag("StartWall")) { _playerNearStart = false; _shopzonemanager._canStartRun = false; }
        if (other.gameObject.CompareTag("TutoWall")) { _playerNearTuto = false; }
        if (other.gameObject.CompareTag("Mstone")) { _canHeal = false; }
        //if (other.gameObject.CompareTag("Chest")) { _canOpenChest = false; }
    }

}
