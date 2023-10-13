using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Redcode.Extensions;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _transform;

    public void Appear()
    {
        _animator.Play("Fly");
        _transform.SetLocalPositionY(5f);
        _transform.DOLocalMoveY(2.5f, 1f);
    }

    public void Attack()
    {
        _animator.Play("Attack");
    }

    public void Disappear()
    {
        _transform.DOLocalMoveY(5f, 1f);
    }
}