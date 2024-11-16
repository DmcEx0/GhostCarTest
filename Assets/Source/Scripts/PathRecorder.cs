using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PathRecorder
{
    private GameConfig _gameConfig;
    private Transform _playerTransform;
    private Queue<Vector3> _pathPoints;
    
    private bool _isRecording;

    public PathRecorder(GameConfig gameConfig, Transform playerTransform)
    {
        _gameConfig = gameConfig;
        _playerTransform = playerTransform;
        
        _pathPoints = new Queue<Vector3>();
    }

    public async UniTask StartRecordPathAsync()
    {
        _isRecording = true;
        
        while(_isRecording)
        {
            await GetDelayMillisecondsAsync(_gameConfig.MillisecondsToStartRegisterPathPoint);
            
            _pathPoints.Enqueue(_playerTransform.position);
            
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