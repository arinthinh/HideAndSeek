using System.Collections;
using System.Collections.Generic;
using Redcode.Extensions;
using UnityEngine;

public class MapObjectsManager : MonoBehaviour
{
    [SerializeField] private float _offSet;
    [SerializeField] private Transform _objectsContainer;

    private List<MapObject> _mapObjectsSpawned = new();

    public void Run(float moveAmount)
    {
        _objectsContainer.Translate(Vector3.left * moveAmount);
    }

    public void Clear()
    {
        foreach (var mapObject in _mapObjectsSpawned)
        {
            Destroy(mapObject.gameObject);
        }
        _mapObjectsSpawned = new();
        _objectsContainer.SetLocalPositionX(0);
    }

    public void SpawnObjects(int level)
    {
        var mapConfig = ConfigManager.Instance.GetConfig<MapConfigCollection>().GetMap(level);
        var objectConfig = ConfigManager.Instance.GetConfig<MapObjectConfigCollection>();

        for (var index = 0; index < mapConfig.objects.Count; index++)
        {
            var objectId = mapConfig.objects[index];
            if (objectId == "") continue;
            var objectPrefab = objectConfig.GetObject(objectId);
            if (objectPrefab != null)
            {
                var objectSpawned = Instantiate(objectPrefab, _objectsContainer);
                var objectPosition = index * _offSet;
                objectSpawned.SetPosition(objectPosition);
                _mapObjectsSpawned.Add(objectSpawned);
            }
        }
    }
}