using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class StopButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action onStartHold;
    public event Action onStopHold;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        onStartHold?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onStopHold?.Invoke();
    }
}
