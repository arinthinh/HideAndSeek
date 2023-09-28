using System;
using System.Collections;
using System.Collections.Generic;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMain : UIView
{
    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        Hide();
        GameplayController.Instance.StartGame();
    }
    
    
}
