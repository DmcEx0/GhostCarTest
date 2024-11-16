using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameObjectFactory
{
    public T Create<T>(T prefab, Vector3 position, Quaternion rotation = default) where T : Object
    {
        return Object.Instantiate(prefab, position, rotation);
    }
}