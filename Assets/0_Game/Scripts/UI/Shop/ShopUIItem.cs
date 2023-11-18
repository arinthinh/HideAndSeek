using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Redcode.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour
{
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

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnItemClick);
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
                DataManager.Instance.OnBuySkin(_skinId, _skinPrice);
                PlayerController.Instance.ChangeSkin(_skinId);
                DataManager.Instance.GameData.currentSkin = _skinId;
                _skinPricePanel.SetActive(false);
                
                // Play select animation
                _selectedPanel.gameObject.SetActive(true);
                _selectedPanel.transform.DOKill();
                _selectedPanel.transform.SetLocalScale(1);
                _selectedPanel.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
            }
            else
            {
                this.Log("Not enough fruits!");
            }
        }
        else
        {
            DataManager.Instance.GameData.currentSkin = _skinId;
            PlayerController.Instance.ChangeSkin(_skinId);
            
            // Play select animation
            _selectedPanel.gameObject.SetActive(true);
            _selectedPanel.transform.DOKill();
            _selectedPanel.transform.SetLocalScale(1);
            _selectedPanel.transform.DOPunchScale(Vector3.one * 0.2f, 0.2f);
        }
    }
}