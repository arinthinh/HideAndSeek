using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JSAM;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewLose : UIView
{
    [SerializeField] private Button _tryAgainButton;
    [SerializeField] private Image _titleImage;

    private void OnEnable()
    {
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClick);
    }

    private void OnDisable()
    {
        _tryAgainButton.onClick.RemoveListener(OnTryAgainButtonClick);
    }

    public override void Init()
    {
        base.Init();
        _titleImage.rectTransform.DOScale(Vector3.one * 1.2f, 1f)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTryAgainButtonClick()
    {
        Hide();
        AudioManager.PlaySound(Sound.Click);
        GameplayController.Instance.Initialize();
    }
}
