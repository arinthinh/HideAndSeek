using DG.Tweening;
using TMPro;
using Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIViewMain : UIView
{
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _startButton;
    [SerializeField] private TextMeshProUGUI _currentFruitTMP;
    [SerializeField] private Image _transitionImage;

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

    public override void Show()
    {
        base.Show();
        _transitionImage.color = Color.black;
        _transitionImage.gameObject.SetActive(true);
        _transitionImage.DOFade(0, 0.3f)
            .SetDelay(0.3f)
            .OnComplete(()=>_transitionImage.gameObject.SetActive(false));
    }

    public override void Hide()
    {
        base.Hide();
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
    
    private void FixedUpdate()
    {
        if(_isShowing)
            _currentFruitTMP.text = DataManager.Instance.GameData.fruits.ToString();
    }
}