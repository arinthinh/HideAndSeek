using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : GameplayObject
{
    [SerializeField] protected Animator _animator;
    
    protected override void HandleAfterTrigger()
    {
        base.HandleAfterTrigger();
        _animator.Play("Fruit_Disappear");
    }
}
