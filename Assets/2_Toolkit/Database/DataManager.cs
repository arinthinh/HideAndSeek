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

    private readonly string _dataKey = "GAME_DATA";
    private GameData _gameData;
    public GameData GameData => _gameData;

    public void Start()
    {
        Load();
        Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_dataKey))
        {
            var dataString = PlayerPrefs.GetString(_dataKey);
            var data = JsonUtility.FromJson<GameData>(dataString);
            _gameData = data;
        }
        else
        {
            _gameData = new();
        }
    }

    public void Save()
    {
        var dataString = JsonUtility.ToJson(_gameData);
        PlayerPrefs.SetString(_dataKey, dataString);
    }
}