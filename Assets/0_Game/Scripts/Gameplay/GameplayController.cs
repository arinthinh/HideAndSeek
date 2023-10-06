using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameplayController : SingletonMono<GameplayController>
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private BossController _boss;
    [SerializeField] private MapController _map;

    private void OnEnable()
    {
        BossController.onScanHitPlayer += HandleLoseGame;
    }

    private void OnDisable()
    {
        BossController.onScanHitPlayer -= HandleLoseGame;
    }

    private void Start()
    {
        Initialize();
    }

    public async void Initialize()
    {
        await UniTask.Yield();
        UIManager.Instance.GetView<UIViewMain>().Show();
    }

    public void StartRun()
    {
        UIManager.Instance.GetView<UIViewGameplay>().Show();
    }
    
    public void HandleWinGame()
    {
        var gameData = DataManager.Instance.GameData;
        gameData.fruits += 10;
        gameData.currentLevel++;
    }

    public void HandleLoseGame()
    {
        _map.Stop();
        UIManager.Instance.GetView<UIViewLose>().Show();
    }

    private void OnObjectTrigger(GameplayObjectType objectType)
    {
        switch (objectType)
        {
            case GameplayObjectType.Obstacles:
                break;
            case GameplayObjectType.Fruit:
                DataManager.Instance.GameData.fruits++;
                break;
            case GameplayObjectType.Slow:
                break;
            case GameplayObjectType.End:
                HandleWinGame();
                break;
        }
    }
}