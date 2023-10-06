using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static event Action onTouchDown;
    public static event Action onTouchUp;
    
    private bool _enable = true;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_enable) return;
        onTouchDown?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!_enable) return;
        onTouchUp?.Invoke();
    }

    public void EnableInput()
    {
        _enable = true;
    }

    public void DisableInput()
    {
        _enable = false;
    }
}
