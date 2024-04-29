using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PauseMenuManager _pausemanager;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private Camera _Cam;
    public CharacterController _ch;

    //COMBAT
    //Dash
    private Vector3 _DashLimit;
    private float _dashDistance = 10f;
    private float _dashCC = 5f;
    private float _CdashCC;
    private float _dashSpeed = 10f;

    private void Awake()
    {
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
            //arreglar el moviment
            //MoveDirection = transform.forward * _dashDistance;
            _CdashCC = _dashCC;
            StartCoroutine(DashCooldown());
        }

        if (Vector3.Distance(transform.position, _DashLimit) > 0.5f)
        {
            _ch.Move(MoveDirection * _dashSpeed * Time.deltaTime);
        }
        
        _ch.Move(MoveDirection * _speed * Time.deltaTime);
    }

    private IEnumerator DashCooldown() {
        Debug.Log("DashCC");
        yield return new WaitForSeconds(_dashCC);
        _CdashCC = 0f;
    }
}
