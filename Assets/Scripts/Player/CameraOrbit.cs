using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraOrbit : MonoBehaviour
{
    [SerializeField] private PauseMenuManager _pausemanager;

    [SerializeField] private GameObject target;
    private float targetDistance = 7f;
    private float rotationX;
    private float rotationY;
    private float cameraLerp = 12f;


    private void Update()
    {
        if (_pausemanager.GamePaused == false)
            CamOrbit();
    }

    private void OnDrawGizmos()
    {
        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);
        Gizmos.DrawLine(target.transform.position, finalPosition);
    }

    private void CamOrbit() {
        rotationX -= Input.GetAxis("Mouse Y");
        rotationY += Input.GetAxis("Mouse X");
        rotationX = Mathf.Clamp(rotationX, -20f, 50f);
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);
        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);
        RaycastHit hit;
        if (Physics.Linecast(target.transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
        }
        transform.position = finalPosition;

    }
}
