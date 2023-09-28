using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTapToContinue : UIView
{
    [SerializeField] private Button _screen;
    [SerializeField] private RectTransform _tapToContinueTransform;

    private void OnEnable()
    {
        _screen.onClick.AddListener(OnScreenClick);
    }

    private void OnDisable()
    {
        _screen.onClick.RemoveAllListeners();
    }

    public override void Show()
    {
        base.Show();
        _tapToContinueTransform
            .DOPunchScale(Vector3.one * 0.2f, 2f)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    public override void Hide()
    {
        base.Hide();
        _tapToContinueTransform.DOKill();
    }

    private void OnScreenClick()
    {
        GameplayController.Instance.ContinueAfterRevive();
    }
}
