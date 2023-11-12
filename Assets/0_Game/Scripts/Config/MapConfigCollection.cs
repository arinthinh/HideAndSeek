using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName ="Maps", menuName = "ScriptableObject/Maps")]
public class MapConfigCollection : GameConfig
{
   public List<MapConfig> configs;

   public MapConfig GetMap(int id)
   {
      var mapGet = configs.FirstOrDefault(map => map.id == id);
      if (mapGet == null)
      {
         var random = Random.Range(0, configs.Count);
         mapGet = configs[random];
      }
      return mapGet;
   }
}

[Serializable]
public class MapConfig
{
   public int id;
   public Transform mapPrefab;
}
