using System;
using UnityEngine;

public enum PlayerState
{
    Hide,
    Run,
    Die,
    Victory
}

public class PlayerController : MonoBehaviour
{
    private PlayerState _playerCurrentState;
    
    
    public void ChangeState(PlayerState state)
    {
        _playerCurrentState = state;
        switch (state)
        {
            case PlayerState.Hide:
                HandleHideState();
                break;
            case PlayerState.Run:
                HandleStateRun();
                break;
            case PlayerState.Die:
                HandleStateDie();
                break;
            case PlayerState.Victory:
                HandleStateVictory();
                break;
        }
    }

    private void HandleHideState()
    {
        
    }

    private void HandleStateRun()
    {
        
    }

    private void HandleStateDie()
    {
        
    }

    private void HandleStateVictory()
    {
        
    }
}