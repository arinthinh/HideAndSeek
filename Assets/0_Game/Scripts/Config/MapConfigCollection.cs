using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="Maps", menuName = "ScriptableObject/Maps")]
public class MapConfigCollection : GameConfig
{
   public List<MapConfig> configs;

   public MapConfig GetMap(int id)
   {
      return configs.FirstOrDefault(map => map.id == id);
   }
}

[Serializable]
public class MapConfig
{
   public int id;
   public Transform mapPrefab;
}
