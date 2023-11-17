using System.Collections;
using System.Collections.Generic;
using TMPro;
using Toolkit.UI;
using UnityEngine;

public class UIViewGameplay : UIView
{
    [SerializeField] private TextMeshProUGUI _currentLevelTMP;
    [SerializeField] private TextMeshProUGUI _currentFruitTMP;
    
    
    public override void Show()
    {
        base.Show();
        _currentLevelTMP.text = "Level" + DataManager.Instance.GameData.currentLevel;
    }

    private void FixedUpdate()
    {
        if(_isShowing)
         _currentFruitTMP.text = DataManager.Instance.GameData.fruits.ToString();
    }
}
