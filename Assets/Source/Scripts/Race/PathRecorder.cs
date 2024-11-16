using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

public class PathRecorder
{
    private readonly GameConfig _gameConfig;
    private readonly Transform _playerTransform;
    private readonly GameObjectFactory _factory;
    
    private List<Vector3> _pathPoints;

    private bool _isRecording;

    public PathRecorder(GameConfig gameConfig, Transform playerTransform, GameObjectFactory factory)
    {
        _gameConfig = gameConfig;
        _playerTransform = playerTransform;
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

    private async UniTask GetDelayMillisecondsAsync(int milliseconds)
    {
        await UniTask.Delay(TimeSpan.FromMilliseconds(milliseconds));
    }
}