using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public sealed class GameplayController : SingletonMono<GameplayController>
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private BossController _boss;
    [SerializeField] private MapController _map;
    [SerializeField] private InputController _input;
    [SerializeField] private CameraController _cameraController;

    private void OnEnable()
    {
        GameplayObject.onTrigger += OnObjectTrigger;
        
        InputController.OnTouchBegin += OnTouch;
        InputController.OnTouchEnd += OnRelease;
        
        BossController.onScanHitPlayer += HandleLoseGame;
    }

    private void OnDisable()
    {
        GameplayObject.onTrigger -= OnObjectTrigger;
        
        InputController.OnTouchBegin -= OnTouch;
        InputController.OnTouchEnd -= OnRelease;
        
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
        _cameraController.Init();;
    }

    public void StartRun()
    {
        UIManager.Instance.GetView<UIViewGameplay>().Show();
        _input.SetEnable(true);
        _map.Move();
        _player.Run();
        _cameraController.ZoomOut();

    }
    
    public void HandleWinGame()
    {
        var gameData = DataManager.Instance.GameData;
        gameData.fruits += 10;
        gameData.currentLevel++;
        
        _map.Stop();
        _player.Stop();
    }

    public void HandleLoseGame()
    {
        //_map.Stop();
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

    private void OnTouch()
    {
        _player.Stop();
        _map.Stop();
    }

    private void OnRelease()
    {
        _player.Run();
        _map.Move();
    }
}