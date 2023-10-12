using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class MapGroundController : MonoBehaviour
{
    [Tooltip("Background 2 position.x")]
    [SerializeField] private float _offset;
    [SerializeField] private Transform _ground1;
    [SerializeField] private Transform _ground2;

    private int _currentBackgroundShow = 1;

    public void Scroll(float moveAmount)
    {
        if (_currentBackgroundShow == 1 && _ground2.position.x <= 0)
        {
            _currentBackgroundShow = 2;
            _ground1.SetPositionX(_offset);
        }
        
        if (_currentBackgroundShow == 2 && _ground1.position.x <= 0)
        {
            _currentBackgroundShow = 1;
            _ground2.SetPositionX(_offset);
        }
        
        _ground1.Translate(Vector3.left * moveAmount);
        _ground2.Translate(Vector3.left * moveAmount);
    }
}
