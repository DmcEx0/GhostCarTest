using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PathRecorder : IPathRecorder, IPathPoints
{
    private readonly GameConfig _gameConfig;
    private readonly GameObjectFactory _factory;
    
    private List<Vector3> _pathPoints;
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
        await GetDelayMillisecondsAsync(_gameConfig.MillisecondsToStartRegisterPathPoint);

        while (_isRecording)
        {
            _pathPoints.Add(_playerTransform.position);
            _ = _factory.Create(_gameConfig.TrackPointPrefab, _playerTransform.position);
            
            await GetDelayMillisecondsAsync(_gameConfig.MillisecondsToRegisterPathPoint);
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

    private async UniTask GetDelayMillisecondsAsync(int milliseconds)
    {
        await UniTask.Delay(TimeSpan.FromMilliseconds(milliseconds));
    }
}