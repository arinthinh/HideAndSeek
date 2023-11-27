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

    private List<Tween> _effectTweens = new();

    private void OnEnable()
    {
        TriggerMapObject.onTrigger += OnObjectTrigger;

        InputController.OnTouchBegin += OnTouch;
        InputController.OnTouchEnd += OnRelease;

        BossController.onScanHitPlayer += HandleLoseGame;
    }

    private void OnDisable()
    {
        TriggerMapObject.onTrigger -= OnObjectTrigger;

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
        UIManager.Instance.HideAllViews();
        UIManager.Instance.GetView<UIViewMain>().Show();
        _input.SetEnable(false);
        _map.Clear();
        _player.Stop();
        _map.Stop();
        _map.SpawnCurrentLevel();
    }

    public void StartRun()
    {
        UIManager.Instance.GetView<UIViewGameplay>().Show();
        _input.SetEnable(true);
        _map.Move();
        _player.Run();
    }

    public void HandleWinGame()
    {
        DataManager.Instance.GameData.fruits += 10;
        DataManager.Instance.GameData.currentLevel++;
        DataManager.Instance.Save();
        _map.Stop();
        _player.Stop();
        UIManager.Instance.GetView<UIViewWin>().Show();
    }

    public void HandleLoseGame()
    {
        //_map.Stop();
        _boss.Hide();
        DOVirtual.DelayedCall(0.5f, () => { UIManager.Instance.GetView<UIViewLose>().Show(); });

        _map.Stop();
        _player.Die();
        _input.SetEnable(false);
    }

    public void RevivePlayer()
    {
        _player.Run();
        _map.Move();
        _input.SetEnable(true);
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
                OnSlow(1);
                break;
            case GameplayObjectType.TriggerBoss:
                _boss.Attack();
                break;
            case GameplayObjectType.End:
                HandleWinGame();
                break;
            case GameplayObjectType.Stun:
                OnStun(2);
                break;
            case GameplayObjectType.Boots:
                OnBoots();
                break;
            case GameplayObjectType.Invi:
                OnInvi();
                break;
            case GameplayObjectType.Blind:
                OnBlind();
                break;
        }
    }

    private void OnSlow(int time)
    {
        _map.Slowdown();
        _effectTweens.Add(DOVirtual.DelayedCall(time, () => { _map.Normalize(); }));
    }

    private void OnStun(int time)
    {
        _map.Stop();
        _player.Stop();
        _input.SetEnable(false);
        _effectTweens.Add(DOVirtual.DelayedCall(time, () =>
        {
            _input.SetEnable(true);
            _map.Normalize();
            _player.Run();
        }));
    }

    private void OnBoots()
    {
        _map.Boots();
        _effectTweens.Add(DOVirtual.DelayedCall(2, _map.Normalize));
    }

    private void OnInvi()
    {
        _boss.OnInvi(true);
        _effectTweens.Add(DOVirtual.DelayedCall(1, () => _boss.OnInvi(false)));
    }

    private void OnBlind()
    {
        UIManager.Instance.GetView<UIViewGameplay>().OnBlind(0.5f);
    }

    private void RemoveAllEffect()
    {
        foreach (var effect in _effectTweens)
        {
            effect.Kill();
        }
        _effectTweens.Clear();
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