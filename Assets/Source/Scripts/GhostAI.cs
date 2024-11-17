using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class GhostAI
{
    private const float AngleOffset = 2f;
    private const float DirectionValue = 1f;
    
    private readonly IPathPoints _pathPoints;

    private Vector3 _nextPoint;
    private int _nextPointIndex = 0;

    private float _currentAngle;
    
    [Inject]
    public GhostAI(IPathPoints pathPoints)
    {
        _pathPoints = pathPoints;
    }

    public float GetDirectionToNextPoint(Transform ghostTransform)
    {
        if(IsNextPointReached(ghostTransform))
        {
            NextPointReached();
        }
        
        Vector3 directionToTarget = _nextPoint - ghostTransform.position;
        directionToTarget.y = 0;

        Vector3 playerForward = ghostTransform.forward;

        _currentAngle = Vector3.SignedAngle(playerForward, directionToTarget, Vector3.up);
        
        if(Mathf.Abs(_currentAngle) > 20f)
        {
            Debug.Log(_currentAngle);
        }

        if (_currentAngle > AngleOffset)
        {
            return DirectionValue;
        }

        if (_currentAngle < -AngleOffset)
        {
            return -DirectionValue;
        }

        return 0;
    }

    public void SetNextPoint()
    {
        _nextPoint = GetNextPoint(_nextPointIndex);
    }

    private void NextPointReached()
    {
        _nextPointIndex++;
        SetNextPoint();
    }
    
    private bool IsNextPointReached(Transform ghostTransform)
    {
        return Vector3.Distance(ghostTransform.position, _nextPoint) < 1f;
    }

    private Vector3 GetNextPoint(int index)
    {
        if(index == _pathPoints.PathPoints.Count)
        {
            return Vector3.zero;
        }
        
        return _pathPoints.PathPoints[index];
    }
}