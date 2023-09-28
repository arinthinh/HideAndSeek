using System;
using UnityEngine;

public enum GameplayState
{
    None,
    Start,
    Run,
    Win,
    AttackGrimace,
    Lose,
    End,
}

public class GameplayController : SingletonMono<GameplayController>
{
    [Header("GAME STATS")]
    [SerializeField] private float _speed;

    [Header("CONTROLLERS")]
    [SerializeField] private MapController _mapController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GrimaceController _grimaceController;
    [SerializeField] private StopButton _stopButton;

    private bool _isReviveAsk;
    private float _progress;

    private GameplayState _currentState;

    private ScreenGameplay _gameplayScreen;

    #region UNITY METHODS

    private void Update()
    {
       
    }

    #endregion

    public void Initialize()
    {
        _gameplayScreen = UIManager.Instance.GetView<ScreenGameplay>();
    }

    public void StartGame()
    {
        ChangeState(GameplayState.Start);
    }

    #region STATE MACHINE

    public void ChangeState(GameplayState state)
    {
        _currentState = state;
        switch (state)
        {
            case GameplayState.None:
                HandleNoneState();
                break;
            case GameplayState.Start:
                HandleStartState();
                break;
            case GameplayState.Run:
                HandleRunState();
                break;
            case GameplayState.Win:
                HandleWinState();
                break;
            case GameplayState.Lose:
                HandleLoseState();
                break;
            case GameplayState.End:
                HandleEndState();
                break;
        }
    }

    private void HandleNoneState()
    {
    }

    private void HandleStartState()
    {
        RegisterEvents();

        _playerController.ChangeState(PlayerState.Run);
        _mapController.StartRun(_speed);
        _gameplayScreen.Show();
        
        ChangeState(GameplayState.Run);

        void RegisterEvents()
        {
            _stopButton.onStartHold += OnStartHoldStopButton;
            _stopButton.onStopHold += OnEndHoldStopButton;
            
            _mapController.onReachedEnd += OnReachedFinishLine;
            _mapController.onTriggerGrimace += OnTriggerGrimace;

            _grimaceController.onScanHitPlayer += OnGrimaceScanHitPlayer;
        }
    }

    private void HandleRunState()
    {
        _playerController.ChangeState(PlayerState.Run);
        _mapController.UnPause();
    }

    private void HandleWinState()
    {
        Debug.Log("Victory");
        _playerController.ChangeState(PlayerState.Victory);
        _mapController.Pause();
        _grimaceController.StopScan();

        UIManager.Instance.GetView<ScreenWin>().Show();
        
        ChangeState(GameplayState.End);
    }

    private void HandleLoseState()
    {
        _mapController.Pause();
        _playerController.ChangeState(PlayerState.Die);

        if (_isReviveAsk)
        {
            _isReviveAsk = true;
            UIManager.Instance.GetView<ScreenRevive>().Show();
        }
        else
        {
            UIManager.Instance.GetView<ScreenLose>().Show();
            HandleEndState();
        }
    }
    
    private void HandleEndState()
    {
        UnRegisterEvents();

        void UnRegisterEvents()
        {
            _stopButton.onStartHold -= OnStartHoldStopButton;
            _stopButton.onStopHold -= OnEndHoldStopButton;

            _mapController.onReachedEnd -= OnReachedFinishLine;
            
            _grimaceController.onScanHitPlayer -= OnGrimaceScanHitPlayer;
            _mapController.onTriggerGrimace -= OnTriggerGrimace;

        }
    }

    #endregion
    
    public void ContinueAfterRevive()
    {
        ChangeState(GameplayState.Run);
    }
    
    #region EVENT METHODS

    private void OnStartHoldStopButton()
    {
        _playerController.ChangeState(PlayerState.Hide);
        _mapController.Pause();
    }

    private void OnEndHoldStopButton()
    {
        _playerController.ChangeState(PlayerState.Run);
        _mapController.UnPause();
    }

    private void OnReachedFinishLine()
    {
        ChangeState(GameplayState.Win);
    }

    private void OnTriggerGrimace()
    {
        _grimaceController.Scan();
    }

    private void OnGrimaceScanHitPlayer()
    {
        ChangeState(GameplayState.Lose);
    }

    #endregion
}

[Serializable]
public class GameplayInfo
{
    public int level;
    public bool isReviveAsk;
}