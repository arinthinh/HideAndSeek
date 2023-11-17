using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameplayObjectType
{
    Obstacles,
    Fruit,
    Slow,
    TriggerBoss,
    End,
    Stun,
    Boots,
    Invi
}

public class TriggerMapObject : MapObject
{
    public static event Action<GameplayObjectType> onTrigger;

    [SerializeField] protected GameplayObjectType _type;
    [SerializeField] protected GameObject _avatar;
    [SerializeField] protected bool _isDisableAfterTrigger = false;
    
    protected bool _isTriggered;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if(_isTriggered) return;
        if (other.CompareTag("Player"))
        {
            _isTriggered = true;
            onTrigger?.Invoke(_type);
            HandleAfterTrigger();
        }
    }

    protected virtual void HandleAfterTrigger()
    {
        if(_isDisableAfterTrigger)
        {
            _avatar.SetActive(false);
        }
        
    }
    
}
