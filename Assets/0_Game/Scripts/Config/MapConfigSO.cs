using System;
using System.Collections.Generic;

public class MapConfigSO : GameConfig
{
   public List<MapConfig> _configs;
}

[Serializable]
public class MapConfig
{
   public int id;
   public List<string> map;
}
