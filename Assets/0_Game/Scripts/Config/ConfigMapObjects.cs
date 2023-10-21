using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MapObjects", menuName = "ScriptableObject/Map Objects")]
public class ConfigMapObjects : GameConfig
{
    public List<ObstacleConfig> configs;
}
[Serializable]
public class ObstacleConfig
{
    public string id;
    public Obstacle prefab;
}
