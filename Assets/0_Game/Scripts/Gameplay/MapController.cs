using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private MapSpawner _mapSpawner;
    [SerializeField] private MapGroundController _mapGround;

    private bool _isMove;
    private float _speedMultiple = 1;

    public void SpawnCurrentLevel()
    {
        _mapSpawner.SpawnMap(DataManager.Instance.GameData.currentLevel);
    }

    public void Move()
    {
        _isMove = true;
    }

    public void Stop()
    {
        _isMove = false;
    }

    public void Slowdown()
    {
        _speedMultiple = 0.5f;
    }

    public void Boots()
    {
        _speedMultiple = 3f;
    }

    public void Normalize()
    {
        _speedMultiple = 1f;
    }

    private void Update()
    {
        if (!_isMove) return;

        var moveAmount = _speed * Time.deltaTime * _speedMultiple;
        _mapGround.Scroll(moveAmount);
        _mapSpawner.OnMapRun(moveAmount);
    }

    public void Clear()
    {
        _mapSpawner.Clear();
    }
}