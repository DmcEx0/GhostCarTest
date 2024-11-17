using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

public class GhostAI
{
    private readonly IPathPoints _pathPoints;

    private AsyncReactiveProperty<Vector3> _nextPoint;
    private int _nextPointIndex = 0;

    public IReadOnlyAsyncReactiveProperty<Vector3> NextPoint => _nextPoint;

    [Inject]
    public GhostAI(IPathPoints pathPoints)
    {
        _pathPoints = pathPoints;
        
        _nextPoint = new AsyncReactiveProperty<Vector3>(GetNextPoint(_nextPointIndex));
    }

    private void SetNextPoint()
    {
        _nextPoint.Value = GetNextPoint(_nextPointIndex);
    }

    private Vector3 GetNextPoint(int index)
    {
        return _pathPoints.PathPoints[index];
    }

    public void NextPointReached()
    {
        _nextPointIndex++;
        
    }
}