using UnityEngine;

namespace GhostRaceTest.Utils
{
    public class GameObjectFactory : IGameObjectFactory
    {
        public T Create<T>(T prefab, Vector3 position, Quaternion rotation = default) where T : Object
        {
            return Object.Instantiate(prefab, position, rotation);
        }
    }
}