using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class AnimationController : MonoBehaviour
{
    public Animator _Playeranimator;
    public PlayerController _PlayerController;

    private void Update()
    {
        _Playeranimator.SetBool("_IsDamaged", _PlayerController._damaged);
        _Playeranimator.SetBool("_IsRunning", _PlayerController._running);
        _Playeranimator.SetBool("_IsAttacking", _PlayerController._attacking);
        _Playeranimator.SetBool("_IsDashing", _PlayerController._dashing);
    }
}
