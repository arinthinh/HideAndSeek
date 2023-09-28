using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Config List Map", fileName = "CfgList_Map")]
public class ConfigListMap : GameConfig
{
    public List<MapConfig> mapList;
}


[Serializable]
public class MapConfig 
{
    
}
