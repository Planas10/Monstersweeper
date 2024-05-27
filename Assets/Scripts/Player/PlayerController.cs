using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PauseMenuManager _pausemanager;
    [SerializeField] private float _speed;
    [SerializeField] private Camera _Cam;
    public CharacterController _ch;

    private float _normalspeed = 5f;

    public Animator _Panimator;

    //COMBAT
    //Player stats
    public int _Phealth;
    public int _basePatkdmg;
    public int _basePdef;
    private int _Patkdmg;
    private int _Pdef;
    public int _PcurrSword;
    public int _PcurrArmor;

    //Dash
    private float _dashCC = 5f;
    private float _CdashCC;
    private float _dashSpeed = 30f;

    private void Awake()
    {
        _speed = _normalspeed;
        _pausemanager = FindObjectOfType<PauseMenuManager>();

        _ch = GetComponent<CharacterController>();
        _CdashCC = 0f;
    }

    private void Update()
    {
        if (_pausemanager.GamePaused == false)
            MovePlayer();
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
            _CdashCC = _dashCC;
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
        
    }
}
