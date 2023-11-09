using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Fruit : TriggerMapObject
{
    [SerializeField] protected Animator _animator;
    
    protected override async void HandleAfterTrigger()
    {
        base.HandleAfterTrigger();
        _animator.Play("Fruit_Disappear");
        await UniTask.Delay(500, DelayType.DeltaTime, cancellationToken: this.destroyCancellationToken);
        gameObject.SetActive(false);
    }
}
