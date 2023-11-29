using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Toolkit.UI;
using UnityEngine;

public class UIViewGameplay : UIView
{
    [SerializeField] private TextMeshProUGUI _currentLevelTMP;
    [SerializeField] private TextMeshProUGUI _currentFruitTMP;
    [SerializeField] private RectTransform _currentLevelTrans;
    [SerializeField] private GameObject _blindPanel;
    
    public override void Init()
    {
        base.Init();
        _currentLevelTrans.transform
            .DOScale(Vector3.one * 1.2f, 0.5f)
            .SetLoops(-1, LoopType.Yoyo);
        _currentLevelTMP.text = "Level " + DataManager.Instance.GameData.currentLevel;
        _currentFruitTMP.text = DataManager.Instance.GameData.fruits.ToString();
    }

    public override void Show()
    {
        base.Show();
        _currentLevelTMP.text = "Level " + DataManager.Instance.GameData.currentLevel;
}

    private void FixedUpdate()
    {
        if(_isShowing)
         _currentFruitTMP.text = DataManager.Instance.GameData.fruits.ToString();
    }

    public void OnBlind(float time)
    {
        _blindPanel.gameObject.SetActive(true);
        DOVirtual.DelayedCall(time, () =>
        {
            _blindPanel.gameObject.SetActive(false);
        });
    }
}
