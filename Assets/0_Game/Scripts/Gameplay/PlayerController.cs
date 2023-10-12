using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public void Init()
    {
        
    }

    public void Run()
    {
        _animator.Play("MaskDude_Run");
    }

    public void Hide()
    {
        _animator.Play("MaskDude_Idle");
    }

    public void Stop()
    {
        _animator.Play("MaskDude_Idle");
    }
    
}
