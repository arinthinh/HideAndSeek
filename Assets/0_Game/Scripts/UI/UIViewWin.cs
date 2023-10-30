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
        _nextLevelButton.onClick.AddListener(OnNextLevelButtonClick);
    }

    private void OnDisable()
    {
        _nextLevelButton.onClick.RemoveListener(OnNextLevelButtonClick);
    }

    private void OnNextLevelButtonClick()
    {
        Hide();
        GameplayController.Instance.Initialize();
    }
}
