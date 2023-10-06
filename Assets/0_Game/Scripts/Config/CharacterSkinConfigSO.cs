using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Cfg_CharacterSkins")]
public class CharacterSkinConfigSO : GameConfig
{
    public List<CharacterSkinConfig> configs;
}

[Serializable]
public class CharacterSkinConfig
{
    public int id;
    public Animator animator;
    public Sprite baseSkin;
    public int price;
}
