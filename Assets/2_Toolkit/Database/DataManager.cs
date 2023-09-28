using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : SingletonMono<DataManager>
{
    public enum SaveType
    {
        None,
        File,
        PlayerPrefs
    }

    [SerializeField] private string _dataKey = "GAME_DATA";
    private GameData _gameData;
    public GameData GameData => _gameData;

    public void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey(_dataKey))
        {
            var dataString = PlayerPrefs.GetString(_dataKey);
            var data = JsonUtility.FromJson<GameData>(dataString);
            _gameData = data;
        }
        else
        {
            CreateNewData();
        }
    }

    public void CreateNewData()
    {
        _gameData = new();
        Save();
    }

    public void Save()
    {
        var dataString = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString(_dataKey, dataString);
    }
}