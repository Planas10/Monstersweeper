using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        LockCursor();
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
