using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PathRecorder : IPathRecorder, IPathPoints
{
    private readonly GameConfig _gameConfig;
    private readonly GameObjectFactory _factory;
    private readonly List<Vector3> _pathPoints;

    private Transform _playerTransform;

    private bool _isRecording;

    public IReadOnlyList<Vector3> PathPoints => _pathPoints;

    public PathRecorder(GameConfig gameConfig, GameObjectFactory factory)
    {
        _gameConfig = gameConfig;
        _factory = factory;

        _pathPoints = new();
    }

    public async UniTask StartRecordPathAsync()
    {
        _isRecording = true;
        AddPathPoint();

        while (_isRecording)
        {
            if (TryRecordNextPoint())
            {
                AddPathPoint();
            }

            await UniTask.NextFrame();
        }
    }
    
    public void AddPathPoint()
    {
        _pathPoints.Add(_playerTransform.position);
        if (_gameConfig.ShowPathPoints)
        {
            _ = _factory.Create(_gameConfig.TrackPointPrefab, _playerTransform.position);
        }
    }

    public void StopRecordPath()
    {
        _isRecording = false;
    }

    public void Reset()
    {
        _pathPoints.Clear();
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private bool TryRecordNextPoint()
    {
        var lastPoint = _pathPoints[_pathPoints.Count - 1];

        if (Vector3.Distance(lastPoint, _playerTransform.position) >= _gameConfig.DistanceBetweenPathPoints)
        {
            return true;
        }

        return false;
    }
}