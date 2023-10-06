using System.Collections;
using System.Collections.Generic;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewWin : UIView
{
    [SerializeField] private Button _nextLevelButton;

    private void OnEnable()
    {
        _nextLevelButton.onClick.AddListener(OnTryAgainButtonClick);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnTryAgainButtonClick);
    }

    private void OnTryAgainButtonClick()
    {
        Hide();
        GameplayController.Instance.Initialize();
    }
}
