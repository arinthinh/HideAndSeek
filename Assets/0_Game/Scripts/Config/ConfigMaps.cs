using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Maps", menuName = "ScriptableObject/Maps")]
public class ConfigMaps : GameConfig
{
   public List<MapConfig> _configs;
}

[Serializable]
public class MapConfig
{
   public int id;
   public List<string> objects;
}
