using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Gameplay _playerinputmap = null;

    [SerializeField] private float _speed = 5f;
    private Vector3 _direction = Vector3.zero;


    private void OnEnable()
    {
        _playerinputmap.Enable();
    }
    private void OnDisable()
    {
        _playerinputmap .Disable();
    }

    private void Awake()
    {


        _playerinputmap = new Gameplay();

        _playerinputmap.Player.Move.performed += ReadInput;
        _playerinputmap.Player.Move.canceled += ReadInput;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void ReadInput(InputAction.CallbackContext context){
        var input = context.ReadValue<Vector2>();
        _direction.x = input.x;
        _direction.z = input.y;
    }

    private void MovePlayer() {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
