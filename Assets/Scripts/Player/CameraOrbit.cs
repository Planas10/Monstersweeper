using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private PauseMenuManager _pausemanager;

    [SerializeField] private GameObject target;
    private float targetDistance = 5f;
    private float rotationX;
    private float rotationY;
    private float cameraLerp = 12f;


    private void Update()
    {
        if (_pausemanager.GamePaused == false)
            CamOrbit();
    }

    private void CamOrbit() {
        rotationX -= Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");
        rotationX = Mathf.Clamp(rotationX, 0f, 50f);
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        transform.position = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);
    }
}
