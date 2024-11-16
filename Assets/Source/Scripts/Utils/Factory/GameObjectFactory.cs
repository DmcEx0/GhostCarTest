using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameObjectFactory
{
    [Inject] private IObjectResolver _objectResolver;
    public T Create<T>(T prefab, Vector3 position, Quaternion rotation = default) where T : Object
    {
        return Object.Instantiate(prefab, position, rotation);
    }
    
    public T CreateWithResolving<T>(T prefab, Vector3 position, Quaternion rotation = default) where T : Component
    {
        return _objectResolver.Instantiate(prefab, position, rotation);
    }
}