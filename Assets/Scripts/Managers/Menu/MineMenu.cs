using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMenu : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void SpawnMine() {
        this.gameObject.SetActive(true);
    }

    public void DespawnMine() {
        this.gameObject.SetActive(false);
    }
}
