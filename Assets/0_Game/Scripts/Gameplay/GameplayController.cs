using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using JSAM;
using UnityEngine;

public sealed class GameplayController : SingletonMono<GameplayController>
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private BossController _boss;
    [SerializeField] private MapController _map;
    [SerializeField] private InputController _input;

    private readonly List<Tween> _effectTweens = new();

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
        AudioManager.PlayMusic(Music.BGM);
        Initialize();
    }

    public async void Initialize()
    {
        await UniTask.Yield();
        
        _input.SetEnable(false);
        _map.Clear();
        _player.Stop();
        _map.Stop();
        _map.SpawnCurrentLevel();
        
        UIManager.Instance.HideAllViews();
        UIManager.Instance.GetView<UIViewMain>().Show();
    }

    public void StartRun()
    {
        AudioManager.PlaySound(Sound.Start);
        
        _map.Move();
        _player.Run();
        _input.SetEnable(true);
        
        UIManager.Instance.GetView<UIViewGameplay>().Show();
    }

    public void HandleWinGame()
    {
        AudioManager.PlaySound(Sound.Win);

        RemoveAllEffect();
        
        _map.Stop();
        _player.Stop();
        _input.SetEnable(false);
        
        DataManager.Instance.OnWinGame();
        UIManager.Instance.GetView<UIViewWin>().Show();
    }

    public void HandleLoseGame()
    {
        AudioManager.PlaySound(Sound.ScanHit);
        RemoveAllEffect();

        _boss.Hide();
        _map.Stop();
        _player.Die();
        _input.SetEnable(false);
        
        DOVirtual.DelayedCall(0.5f, () => { UIManager.Instance.GetView<UIViewLose>().Show(); });
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
                AudioManager.PlaySound(Sound.FruitCollect);
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
                OnStun(1.5f);
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
        AudioManager.PlaySound(Sound.Slow);
        _map.Slowdown();
        _effectTweens.Add(DOVirtual.DelayedCall(time, () => { _map.Normalize(); }));
    }

    private void OnStun(float time)
    {
        AudioManager.PlaySound(Sound.Eat);

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
        AudioManager.PlaySound(Sound.Boots);
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
        AudioManager.PlaySound(Sound.Hide);
        _player.Stop();
        _map.Stop();
    }

    private void OnRelease()
    {
        _player.Run();
        _map.Move();
    }
}