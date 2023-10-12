using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MapObjectsManager _mapObjectsManager;
    [SerializeField] private MapGroundController _mapGround;

    private bool _isMove;
    private float _speedMultiple = 1;
    
    public void Move()
    {
        _isMove = true;
    }

    public void Stop()
    {
        _isMove = false;
    }

    public void Slowdown(float multiple)
    {
        _speedMultiple *= multiple;
    }
    
    private void Update()
    {
        if(!_isMove) return;
        
        var moveAmount = _speed * Time.deltaTime * _speedMultiple;
        _mapGround.Scroll(moveAmount);
        _mapObjectsManager.Run(moveAmount);
    }
}