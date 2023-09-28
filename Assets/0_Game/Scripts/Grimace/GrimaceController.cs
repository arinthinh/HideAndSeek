using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GrimaceController : MonoBehaviour
{
    public event Action onScanHitPlayer;

    [SerializeField] private float _scanSpeed;
    
    [SerializeField] private FOV _fovLeft;
    [SerializeField] private FOV _fovRight;

    private bool _isScanning;
    private float _scanRadius;

    private void Update()
    {
        OnScanning();
    }

    public void Scan()
    {
        _fovLeft.viewRadius = 0;
        _fovRight.viewRadius = 0;
        _scanRadius = 0;
        _isScanning = true;
    }

    public void StopScan()
    {
        _isScanning = false;
        DOVirtual.Float(10, 0, 2f, value =>
        {
            _fovLeft.viewRadius = value;
            _fovRight.viewRadius = value;
        });
    }

    public void StopAllActions()
    {
    }

    private void OnScanning()
    {
        if (_isScanning)
        {
            _scanRadius += Time.deltaTime * _scanSpeed;
            _fovLeft.viewRadius = _scanRadius;
            _fovRight.viewRadius = _scanRadius;
            if (_fovLeft.visibleTargets.Count > 0 || _fovRight.visibleTargets.Count > 0)
            {
                _isScanning = false;
                _fovLeft.viewRadius = 0;
                _fovRight.viewRadius = 0;
                onScanHitPlayer?.Invoke();
                return;
            }

            if (_scanRadius >= 10)
            {
                StopScan();
                return;
            }
            
        }
    }
}