using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MapObjectsManager _mapObjectsManager;
    [SerializeField] private MapGroundController _mapGround;

    private void Update()
    {
        var moveAmount = _speed * Time.deltaTime;
        _mapGround.Scroll(moveAmount);
    }
}