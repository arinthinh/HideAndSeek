using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="CharacterSkins", menuName = "ScriptableObject/Character Skins")]
public class CharacterSkinConfigCollection : GameConfig
{
    public List<CharacterSkinConfig> configs;

    public CharacterSkinConfig GetSkin(int id)
    {
        return configs.FirstOrDefault(skin => skin.id == id);
    }
}

[Serializable]
public class CharacterSkinConfig
{   
    public int id;
    public RuntimeAnimatorController  animator;
    public Sprite baseSkin;
    public int price;
}
