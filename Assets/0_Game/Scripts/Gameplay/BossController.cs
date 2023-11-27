using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JSAM;
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

    // Private fields
    private bool _isScanning;
    private float _scanRadius;
    private bool _isInvi;

    private Tween _scanTween;
    private Tween _stopScanTween;

    // Methods
    public void Init()
    {
    }

    public void Attack()
    {
        // Animation
        _bossAnimator.Appear();

        // FOV
        _fovLeft.viewRadius = 0;
        _scanRadius = 0;
        _isScanning = true;

        _scanTween = DOVirtual
            .Float(0, _scanMaxRadius, _scanTime, value =>
            {
                _fovLeft.viewRadius = value;
                CheckIsHitPlayer();
            })
            .SetDelay(_scanDelay)
            .OnStart(() =>
            {
                _bossAnimator.Attack();
                AudioManager.PlaySound(Sound.Lazer);
            })
            .OnComplete(StopAttack);
    }

private void CheckIsHitPlayer()
{
    if (_isInvi) return;
    if (_fovLeft.visibleTargets.Count > 0)
    {
        onScanHitPlayer?.Invoke();
        StopAttack();
    }
}

public void StopAttack()
{
    _isScanning = false;
    _scanTween?.Kill();
    _fovLeft.viewRadius = 0;
    _bossAnimator.Disappear();
}

public void OnInvi(bool isInvi)
{
    _isInvi = isInvi;
}

public void Hide()
{
    _bossAnimator.Disappear();
}
}