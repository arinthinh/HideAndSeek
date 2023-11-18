using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using DG.Tweening;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewWin : UIView
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private GameObject _contentPanel;
    [SerializeField] private UIParticle _winFx;
    public override void Show()
    {
        base.Show();
        _winFx.RefreshParticles();
        _winFx.Play();
        _contentPanel.gameObject.SetActive(false);
        DOVirtual.DelayedCall(2f, () => _contentPanel.gameObject.SetActive(true));
    }

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