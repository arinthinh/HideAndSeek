using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Transform _ground1;
    [SerializeField] private Transform _ground2;
    [SerializeField] private Transform _obstaclesContainer;

    private bool _isRun;
    
    private void Update()
    {
        if (!_isRun) return;
        
        
    }

    public void Stop()
    {
        
    }
}
