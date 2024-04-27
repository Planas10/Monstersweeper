using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PauseMenuManager _pausemanager;

    [SerializeField] private float _speed = 5f;

    [SerializeField] private Camera _Cam;


    public CharacterController _ch;

    private void Awake()
    {
        _ch = GetComponent<CharacterController>();
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
    }
}
