using UnityEngine;

namespace GhostRaceTest.Utils
{
    public interface IGameObjectFactory
    {
        public T Create<T>(T prefab, Vector3 position, Quaternion rotation = default) where T : Object;

    }
}