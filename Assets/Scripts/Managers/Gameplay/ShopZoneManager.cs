using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopZoneManager : MonoBehaviour
{
    [Tooltip("Shop zone ambience music")]
    [SerializeField] private AudioSource _Smus;


    private void Awake()
    {
        LockCursor();
        _Smus.Play();
    }

    //Hide cursor
    public void LockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    //Unhide cursor
    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
    }
}
