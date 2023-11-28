using System;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewRevive : UIView
{
    [SerializeField] private Button _reviveButton;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _reviveButton.onClick.AddListener(OnReviveButtonClick);
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _reviveButton.onClick.RemoveListener(OnReviveButtonClick);
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnReviveButtonClick()
    {
        GameManager.Instance.RevivePlayer();
    }

    private void OnCloseButtonClick()
    {
        UIManager.Instance.GetView<UIViewLose>().Show();
    }
}