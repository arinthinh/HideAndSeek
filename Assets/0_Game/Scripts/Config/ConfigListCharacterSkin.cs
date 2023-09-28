using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Config List Character Skin", fileName = "CfgList_CharacterSkin")]
public class ConfigListCharacterSkin : GameConfig
{
    public List<CharacterSkinConfig> skinList;
}

[Serializable]
public class CharacterSkinConfig
{
    public int id;
    //public SkeletonAsset skin;
}