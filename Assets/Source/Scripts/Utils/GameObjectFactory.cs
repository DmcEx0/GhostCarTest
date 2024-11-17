using UnityEngine;

namespace GhostRaceTest.Utils
{
    public class GameObjectFactory
    {
        public T Create<T>(T prefab, Vector3 position, Quaternion rotation = default) where T : Object
        {
            return Object.Instantiate(prefab, position, rotation);
        }
    }
}