using System;
using System.Collections;
using System.Collections.Generic;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewLose : UIView
{
    [SerializeField] private Button _tryAgainButton;

    private void OnEnable()
    {
        _tryAgainButton.onClick.AddListener(OnTryAgainButtonClick);
    }

    private void OnDisable()
    {
        _tryAgainButton.onClick.RemoveListener(OnTryAgainButtonClick);
    }

    private void OnTryAgainButtonClick()
    {
        Hide();
        GameplayController.Instance.Initialize();
    }
}
