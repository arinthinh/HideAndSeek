using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    [SerializeField] private GameData _gameData;

    private const string DATA_KEY = "GAME_DATA";
    public GameData GameData => _gameData;

    public void Init()
    {
        Load();
        Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(DATA_KEY)) UpdateData();
        else CreateNewData();
    }

    public void UpdateData()
    {
        var dataString = PlayerPrefs.GetString(DATA_KEY);
        var oldData = JsonUtility.FromJson<GameData>(dataString);
        _gameData = oldData;
    }

    public void CreateNewData()
    {
        _gameData = new();
    }

    public void Save()
    {
        var dataString = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString(DATA_KEY, dataString);
    }

    // DATA HELPERS
    public void OnBuySkin(int skinId, int skinPrice)
    {
        _gameData.fruits -= skinPrice;
        if (!_gameData.skinOwned.Contains(skinId))
        {
            _gameData.skinOwned.Add(skinId);
        }
        Save();
    }

    public void OnWinGame()
    {
        _gameData.currentLevel++;
        _gameData.fruits += 10;
        Save();
    }
}