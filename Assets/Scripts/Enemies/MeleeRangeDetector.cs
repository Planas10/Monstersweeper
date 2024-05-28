using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangeDetector : MonoBehaviour
{
    [SerializeField] private MonsterScript _monsterScript;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        { 
            _monsterScript._PlayerInMeleeRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _monsterScript._PlayerInMeleeRange = false;
        }
    }
}
