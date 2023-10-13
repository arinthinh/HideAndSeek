using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // Events
    public static event Action onScanHitPlayer;

    [Header("ANIMATOR")]
    [SerializeField] private BossAnimator _bossAnimator;

    [Header("SCAN FOV")]
    [SerializeField] private float _scanTime;
    [SerializeField] private float _scanMaxRadius;
    [SerializeField] private float _scanDelay;
    [Space(20)]
    [SerializeField] private FOV _fovLeft;
    [SerializeField] private FOV _fovRight;
    [SerializeField] private GameObject _eyeLightLeft;
    [SerializeField] private GameObject _eyeLightRight;

    // Private fields
    private bool _isScanning;
    private float _scanRadius;

    private Tween _scanTween;
    private Tween _stopScanTween;

    // Methods
    public void Init()
    {
    }

    public void Attack()
    {
        // Animation
        _bossAnimator.Attack();

        // FOV
        _fovLeft.viewRadius = 0;
        _fovRight.viewRadius = 0;
        _scanRadius = 0;
        _isScanning = true;

        _scanTween = DOVirtual
            .Float(0, _scanMaxRadius, _scanTime, value =>
            {
                _fovLeft.viewRadius = value;
                _fovRight.viewRadius = value;
                CheckIsHitPlayer();
            })
            .SetDelay(_scanDelay)
            .OnStart(() =>
            {
                _eyeLightLeft.SetActive(true);
                _eyeLightRight.SetActive(true);
            })
            .OnComplete(() => StopAttack());
    }

    public void FakeScan()
    {
    }

    private void CheckIsHitPlayer()
    {
        if (_fovLeft.visibleTargets.Count > 0 || _fovRight.visibleTargets.Count > 0)
        {
            onScanHitPlayer?.Invoke();
            StopAttack(false);
        }
    }

    public void StopAttack(bool isDisappear = true)
    {
        _isScanning = false;
        _scanTween?.Kill();
        _fovLeft.viewRadius = 0;
        _fovRight.viewRadius = 0;
        _eyeLightLeft.SetActive(false);
        _eyeLightRight.SetActive(false);
        if (isDisappear)
        {
            _bossAnimator.Disappear();
        }
    }
}