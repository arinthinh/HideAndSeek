using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterSkins", menuName = "ScriptableObject/Character Skins")]
public class ConfigCharacterSkins : GameConfig
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
