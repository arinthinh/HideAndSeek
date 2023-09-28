using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapPointType
{
    None,
    TriggerGrimace,
    End
}

public class MapPoint : MonoBehaviour
{
    [SerializeField] private MapPointType _mapPointType;
    
    public event Action<MapPointType> onTrigger;
    
    private Transform _transform;
    private bool _isTriggered;

    private void Start()
    {
        _transform = transform;
    }

    public void OnActive()
    {
        if (!_isTriggered)
        {
            if (_transform.position.x <= 0)
            {
                _isTriggered = true;
                onTrigger?.Invoke(_mapPointType);
            }
        }
    }
}
