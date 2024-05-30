using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineField : MonoBehaviour
{

    Vector2Int _id;
    bool _isMine;
    Action<int, int> _onClick;
    bool _isShowed;
    SpriteRenderer _sr;

    public bool IsShowed { get { return _isShowed; } set { _isShowed = value; } }
    public bool IsMine { get { return _isMine; } }
    public Sprite sprite {set{ _sr.sprite = value; }}

    public void Init(Vector2Int id, bool _isMine, Action<int, int> _onClick) {
        _sr = GetComponent<SpriteRenderer>();
        this._id = id;
        this._isMine = _isMine;
        this._onClick = _onClick;
        _isShowed = false;
    }

    private void OnMouseDown()
    {
        _onClick(_id.x, _id.y);
    }
}
