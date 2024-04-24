using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private Camera _Cam;
    private float _Hmouse;
    private float _Vmouse;

    private float _mousehorizontal = 2f;
    private float _mousevertical = 2f;

    public CharacterController _ch;

    private void Awake()
    {
        _ch = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MouseMove();
        MovePlayer();
        Debug.Log(_Vmouse);
    }

    private void MovePlayer() {

        Vector3 MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        MoveDirection = transform.TransformDirection(MoveDirection);
        _ch.Move(MoveDirection * _speed * Time.deltaTime);
    }

    private void MouseMove()
    {
        _Hmouse = _mousehorizontal * Input.GetAxis("Mouse X");
        _Vmouse = _mousevertical * -Input.GetAxis("Mouse Y");

        Mathf.Clamp(_Cam.transform.rotation.x, -45, 45);

        transform.Rotate(0, _Hmouse, 0);
        _Cam.transform.Rotate(_Vmouse, 0, 0,Space.Self);

    }
}
