using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    [SerializeField] private float _offSet;

    private Transform _map;

    public void OnMapRun(float moveAmount)
    {
        _map.Translate(Vector3.left * moveAmount);
    }

    public void Clear()
    {
        if (_map != null)
        {
            Destroy(_map.gameObject);
        }
        _map = null;
    }

    public void SpawnMap(int level)
    {
        var mapConfig = ConfigManager.Instance.GetConfig<MapConfigCollection>().GetMap(level);
        _map = Instantiate(mapConfig.mapPrefab, transform);
    }
}