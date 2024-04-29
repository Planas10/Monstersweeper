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
    private float _dashCC = 5f;
    private float _CdashCC;
    private float _dashDistance;

    private void Awake()
    {
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
        _ch.Move(MoveDirection * _speed * Time.deltaTime);

        //Combat Controls
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && _CdashCC == 0f)
        {
            Debug.Log("Dash");
            //arreglar el moviment
            _ch.Move((MoveDirection + transform.forward) * _speed * Time.deltaTime);
            _CdashCC = _dashCC;
            StartCoroutine(DashCooldown());
        }
    }

    private IEnumerator DashCooldown() {
        Debug.Log("DashCC");
        yield return new WaitForSeconds(_dashCC);
        _CdashCC = 0f;
    }
}
