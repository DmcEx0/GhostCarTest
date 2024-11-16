using System.Collections.Generic;
using UnityEngine;

public interface IPathPoints
{
    public IReadOnlyList<Vector3> PathPoints { get; }
}