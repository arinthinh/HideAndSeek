using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMono<PlayerController>
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;

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

    public void ChangeSkin(int id)
    {
        var config = ConfigManager.Instance.GetConfig<CharacterSkinConfigCollection>().GetSkin(id);
        _spriteRenderer.sprite = config.baseSkin;
        _animator.runtimeAnimatorController = config.animator;
    }
}