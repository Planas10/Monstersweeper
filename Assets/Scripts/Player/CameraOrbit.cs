using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraOrbit : MonoBehaviour
{
    private float _rotSpeed = 500f;
    private float _Hmouse;
    private float _Vmouse;

    private float _mousehorizontal = 2f;
    private float _mousevertical = 2f;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "FightScene")
            CamOrbit();
        else
            CameraMovement();
    }

    private void CamOrbit() {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float _verticalinput = Input.GetAxis("Mouse Y") * _rotSpeed * Time.deltaTime;
            float _horizontalinput = Input.GetAxis("Mouse X") * _rotSpeed * Time.deltaTime;

            transform.Rotate(Vector3.right, _verticalinput);
            transform.Rotate(Vector3.up, _horizontalinput, Space.World);
        }
    }

    private void CameraMovement()
    {
        _Hmouse = _mousehorizontal * Input.GetAxis("Mouse X");
        _Vmouse = _mousevertical * -Input.GetAxis("Mouse Y");

        Mathf.Clamp(transform.rotation.x, -45, 45);

        transform.Rotate(0, _Hmouse, 0);
        transform.Rotate(_Vmouse, 0, 0, Space.Self);

    }
}
