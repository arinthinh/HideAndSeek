using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public event Action onReachedEnd;
    public event Action onTriggerGrimace;

    [SerializeField] private Transform _obstaclesContainer;
    [SerializeField] private List<MapPoint> _scanPoints;

    private MapConfig _currentMapConfig;

    private bool _isRun;
    private float _speed;


    public void Initialize()
    {
        
    }

    public void LoadMapConfig(MapConfig mapConfig)
    {
        _currentMapConfig = mapConfig;
    }

    public void Update()
    {
        if (_isRun)
        {
            float moveAmount = _speed * Time.deltaTime;
            _obstaclesContainer.Translate(Vector3.left * moveAmount);

            foreach (var point in _scanPoints)
            {
                point.OnActive();
            }
        }
    }

    public void StartRun(float initializeSpeed)
    {
        _speed = initializeSpeed;
        _isRun = true;
        foreach (var point in _scanPoints)
        {
            point.onTrigger += OnPointTrigger;
        }
    }

    public void Pause()
    {
        _isRun = false;
    }

    public void UnPause()
    {
        _isRun = true;
    }

    public void StopAllActions()
    {
        _isRun = false;
        foreach (var point in _scanPoints)
        {
            point.onTrigger -= OnPointTrigger;
        }
    }

    private void OnPointTrigger(MapPointType pointType)
    {
        switch (pointType)
        {
            case MapPointType.TriggerGrimace:
                onTriggerGrimace?.Invoke();
                break;
            case MapPointType.End:
                onReachedEnd?.Invoke();
                break;
        }
    }
}