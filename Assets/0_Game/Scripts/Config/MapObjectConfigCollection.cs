using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="MapObjects", menuName = "ScriptableObject/Map Objects")]
public class MapObjectConfigCollection : GameConfig
{
    public List<MapObject> mapObjects;

    public MapObject GetObject(string objectName)
    {
        return mapObjects.FirstOrDefault(mObject => mObject.name == objectName);
    }
}
