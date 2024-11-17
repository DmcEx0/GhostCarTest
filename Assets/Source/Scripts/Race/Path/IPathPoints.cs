using System.Collections.Generic;
using UnityEngine;

namespace GhostRaceTest.Race.Path
{
    public interface IPathPoints
    {
        public IReadOnlyList<Vector3> PathPoints { get; }
    }
}