using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PauseMenuManager _pausemanager;
    [SerializeField] private float _speed;
    [SerializeField] private Camera _Cam;
    public CharacterController _ch;
    public MonsterScript _Mscript;
    public GameManager _gamemanager;

    public GameObject _CamPH;

    private float _normalspeed = 10f;

    public Animator _Panimator;

    //COMBAT
    //Player stats
    public int _Phealth;
    //public int _basePdef;
    //private int _Pdef;
    //public int _PcurrSword;
    //public int _PcurrArmor;

    //Dash
    private float _dashCC = 5f;
    private float _CdashCC;
    private float _dashSpeed = 30f;

    //Attack
    private bool _EnemyInRange;
    //public int _basePatkdmg = 10;
    public int _Patkdmg;
    private float _attkCC = 2.5f;
    private float _CattkCC;

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
        
        //if (Physics.Raycast(transform.position, new Vector3(_CamPH.transform.position.x, _CamPH.transform.position.y, _CamPH.transform.position.z + 3), out RaycastHit hit, 6)) {
        //    Debug.Log("Patatudo");
        //}
    }

    private void MovePlayer() {

        Vector3 MoveDirection = Quaternion.Euler(0f, _Cam.transform.eulerAngles.y, 0f) * new Vector3(Input.GetAxis("Horizontal"), -3f, Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);

        //Combat Controls
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && _CdashCC == 0f)
        {
            Debug.Log("Dash");
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
        Debug.Log("DashCC");
        yield return new WaitForSeconds(_dashCC);
        Debug.LogError("DashReady");
        _CdashCC = 0f;
    }

    private void Attack() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _CattkCC == 0f)
        {
            Debug.LogError("Attack");
            _CattkCC = +1;
            if (_EnemyInRange)
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
    }
    private IEnumerator AttackCooldown()
    {
        Debug.Log("AttkCC");
        yield return new WaitForSeconds(_attkCC);
        Debug.LogError("AttkReady");
        _CattkCC = 0f;
    }
}
