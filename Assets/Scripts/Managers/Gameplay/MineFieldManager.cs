using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineFieldManager : MonoBehaviour
{
    MineField[,] _minefield;

    public Vector2Int _dimensions;
    public MineField _prefab;
    [Range(0,100)]
    public int _minespercent;

    public GameObject _PlayerGUI;

    public Sprite _normalcell; //bloque sin tocar
    public Sprite _freecell; //bloque sin mina descubierto

    private void Start()
    {
        initMinefield();
    }

    private void initMinefield() {
        if (_minefield == null)
        {
            _minefield = new MineField[_dimensions.x, _dimensions.y];
            MinefieldLoop((i, j) => {
                MineField minefield = Instantiate(_prefab, new Vector3(i - _dimensions.x / 2, j - _dimensions.y / 2), Quaternion.identity, transform);
                minefield.transform.SetParent(_PlayerGUI.transform, false);
                minefield.name = string.Format("(x: {0}, y: {1})", i, j);
                _minefield[i, j] = minefield;
            });
        }
        MinefieldLoop((i, j) => {
            _minefield[i, j].Init(new Vector2Int(i, j),
                UnityEngine.Random.Range(0, 100) < _minespercent ? true: false, Activate);
            _minefield[i, j].sprite = _normalcell;
        });
    }

    void Activate(int i, int j) {
        print(string.Format("(x: {0}, y: {1})", i, j));
    }

    bool PointIsInsideMatrix(int i, int j)
    {
        if (i >= _minefield.GetLength(0))
            return false;
        if (i < 0)
            return false;
        if (j >= _minefield.GetLength(1))
            return false;
        if (j < 0)
            return false;

        return true;
    }

    private void MinefieldLoop(Action<int, int> e) {
        for (int i = 0; i < _dimensions.x; i++)
        {
            for (int j = 0; j < _dimensions.y; j++)
            {
                e(i, j);
            }
        }
    }
}
