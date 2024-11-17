using UnityEngine;
using VContainer;

public class GhostAI
{
    private readonly IPathPoints _pathPoints;

    private Vector3 _nextPoint;
    private int _nextPointIndex = 0;
    
    [Inject]
    public GhostAI(IPathPoints pathPoints)
    {
        _pathPoints = pathPoints;
    }
    
    public void Start()
    {
        _nextPoint = GetNextPoint(_nextPointIndex);
    }
    
    public void NextPointReached()
    {
        _nextPointIndex++;
        SetNextPoint();
    }

    public float GetDirectionToNextPoint(Transform ghostTransform)
    {
        Vector3 directionToTarget = _nextPoint - ghostTransform.position;
        directionToTarget.y = 0;

        Vector3 playerForward = ghostTransform.forward;

        float angle = Vector3.SignedAngle(playerForward, directionToTarget, Vector3.up);

        if (angle > 0)
        {
            return 1;
        }
        else if (angle < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private void SetNextPoint()
    {
        _nextPoint = GetNextPoint(_nextPointIndex);
    }

    private Vector3 GetNextPoint(int index)
    {
        return _pathPoints.PathPoints[index];
    }
}