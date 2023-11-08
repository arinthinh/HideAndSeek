using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class MapObjectsManager : MonoBehaviour
{
    [SerializeField] private float _offSet;
    
    private Transform _map;

    public void Run(float moveAmount)
    {
        _map.Translate(Vector3.left * moveAmount);
    }

    public void Clear()
    {
        Destroy(_map);
        _map = null;
    }

    public void SpawnObjects(int level)
    {
        var mapConfig = ConfigManager.Instance.GetConfig<MapConfigCollection>().GetMap(level); 
        _map = Instantiate(mapConfig.mapPrefab, transform);
    }
}