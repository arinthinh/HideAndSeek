using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JSAM;
using Redcode.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour
{
    private static event Action<int> onSelect;

    [SerializeField] private int _skinId;
    [SerializeField] private int _skinPrice;
    [SerializeField] private GameObject _skinPricePanel;
    [SerializeField] private GameObject _selectedPanel;

    private Button _button;
    private bool _isLock;

    // Private methods
    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnItemClick);
        onSelect += CheckOtherItemSelect;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnItemClick);
        onSelect -= CheckOtherItemSelect;
    }

    private async void Start()
    {
        await UniTask.Yield();
        LoadData();
    }

    private void LoadData()
    {
        _isLock = !DataManager.Instance.GameData.skinOwned.Contains(_skinId);
        _skinPricePanel.SetActive(_isLock);
        _selectedPanel.gameObject.SetActive(DataManager.Instance.GameData.currentSkin == _skinId);
    }


    private void CheckOtherItemSelect(int id)
    {
        _selectedPanel.gameObject.SetActive(DataManager.Instance.GameData.currentSkin == _skinId);
    }

    // Event listeners
    private void OnItemClick()
    {
        if (_isLock)
        {
            var currentFruit = DataManager.Instance.GameData.fruits;
            // If have enough fruits
            if (currentFruit >= _skinPrice)
            {
                _isLock = false;
                AudioManager.PlaySound(Sound.Buy);
                DataManager.Instance.OnBuySkin(_skinId, _skinPrice);
                PlayerController.Instance.ChangeSkin(_skinId);
                DataManager.Instance.GameData.currentSkin = _skinId;
                _skinPricePanel.SetActive(false);

                // Play select animation
                _selectedPanel.gameObject.SetActive(true);
                _selectedPanel.transform.DOKill();
                _selectedPanel.transform.SetLocalScale(1);
                _selectedPanel.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
                
                onSelect?.Invoke(_skinId);
            }
            else
            {
                AudioManager.PlaySound(Sound.CantBuy);
                this.Log("Not enough fruits!");
            }
        }
        else
        {
            AudioManager.PlaySound(Sound.Click);
            DataManager.Instance.GameData.currentSkin = _skinId;
            PlayerController.Instance.ChangeSkin(_skinId);

            // Play select animation
            _selectedPanel.gameObject.SetActive(true);
            _selectedPanel.transform.DOKill();
            _selectedPanel.transform.SetLocalScale(1);
            _selectedPanel.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
            
            onSelect?.Invoke(_skinId);
        }
    }
}