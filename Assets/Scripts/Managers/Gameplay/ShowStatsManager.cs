using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStatsManager : MonoBehaviour
{
    [SerializeField] private GameObject _StatsCanvas;
    private void Awake()
    {
        _StatsCanvas.SetActive(false);
    }

    private void Update()
    {
        ShowStats();
    }

    private void ShowStats() {
        if (Input.GetKey(KeyCode.Tab))
        {
            _StatsCanvas.SetActive(true);
        }
        else
        {
            _StatsCanvas.SetActive(false);
        }
    }
}
