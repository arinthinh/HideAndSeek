using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIItem : MonoBehaviour
{
    [SerializeField] private int _skinId;
    [SerializeField] private int _skinPrice;
    [SerializeField] private GameObject _skinPricePanel;

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
        if (_isLock)
        {
            _skinPricePanel.SetActive(true);
        }
        else
        {
            _skinPricePanel.SetActive(false);
        }
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
            if (currentFruit < _skinPrice)
            {
                
            }
            else
            {
                PlayerController.Instance.ChangeSkin(_skinId);
                _skinPricePanel.SetActive(false);
            }
        }
        else
        {
            PlayerController.Instance.ChangeSkin(_skinId);
        }
        
    }
}