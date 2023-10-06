using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewMain : UIView
{
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _startButton;

    private void OnEnable()
    {
        _settingButton.onClick.AddListener(OnSettingButtonClick);
        _shopButton.onClick.AddListener(OnShopButtonClick);
        _startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnDisable()
    {
        _settingButton.onClick.RemoveAllListeners();
        _shopButton.onClick.RemoveAllListeners();
        _startButton.onClick.RemoveAllListeners();
    }

    private void OnSettingButtonClick()
    {
        UIManager.Instance.GetView<UIViewSetting>().Show();
    }

    private void OnShopButtonClick()
    {
        UIManager.Instance.GetView<UIViewShop>().Show();
    }

    private void OnStartButtonClick()
    {
        Hide();
        GameplayController.Instance.StartRun();
    }
}