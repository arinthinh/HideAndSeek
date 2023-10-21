using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Normal,
    EndPoint,
    Trigger,
    Slow,
}

public class Obstacle : MonoBehaviour
{
    public static Action<ObstacleType> onEnter;
    public static Action<ObstacleType> onExit;

    [SerializeField] protected ObstacleType _type;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RaiseEnterEvent();
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RaiseExitEvent();
        }
    }

    protected void RaiseEnterEvent()
    {
        onEnter?.Invoke(_type);
    }

    protected void RaiseExitEvent()
    {
        onExit?.Invoke(_type);
    }
}