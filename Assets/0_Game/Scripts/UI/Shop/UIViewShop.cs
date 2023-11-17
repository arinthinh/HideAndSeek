using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewShop : UIView
{
     [SerializeField] private Button _closeButton;
     [SerializeField] private TextMeshProUGUI _currentFruitTMP;

     private void OnEnable()
     {
          _closeButton.onClick.AddListener(OnCloseButtonClick);
     }

     private void OnDisable()
     {
          _closeButton.onClick.AddListener(OnCloseButtonClick);
     }

     private void OnCloseButtonClick()
     {
          Hide();
     }
     
     private void FixedUpdate()
     {
          if(_isShowing)
               _currentFruitTMP.text = DataManager.Instance.GameData.fruits.ToString();
     }
}
