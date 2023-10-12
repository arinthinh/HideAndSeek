using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    public static event Action OnTouchBegin; // Event for touch beginning
    public static event Action OnTouchEnd; // Event for touch ending

    private bool _isEnable;
    

    public void SetEnable(bool isEnable)
    {
        _isEnable = isEnable;
    }

    #if UNITY_EDITOR
    private void Update()
    {
        if (!_isEnable) return;
        
        if(Input.GetMouseButtonDown(0))
        {
            OnTouchBegin?.Invoke();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            OnTouchEnd?.Invoke();
        }
    }
    #else
    private void Update()
    {
        if (!_isEnable) return;
        // Check if there is any touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Check the phase of the touch
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Invoke the OnTouchBegin event
                    OnTouchBegin?.Invoke();
                    break;

                case TouchPhase.Ended:
                    // Invoke the OnTouchEnd event
                    OnTouchEnd?.Invoke();
                    break;
            }
        }
    }
    #endif
    
    
}