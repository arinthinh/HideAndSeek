using System;
using System.Collections;
using System.Collections.Generic;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class ScreenAttackBossAsk : UIView
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _acceptButton.onClick.AddListener(OnAcceptButtonClick);
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _acceptButton.onClick.RemoveAllListeners();
        _closeButton.onClick.RemoveAllListeners();
    }

    private void OnAcceptButtonClick()
    {
        
    }

    private void OnCloseButtonClick()
    {
        
    }
}
