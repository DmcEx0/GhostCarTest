using Ashsvp;
using Cinemachine;
using UnityEngine;
using VContainer;

public class CarSpawner
{
    private readonly GameConfig _gameConfig;
    private readonly GameObjectFactory _factory;
    private readonly Transform _playerSpawnPoint;
    private readonly CinemachineVirtualCamera _virtualCamera;
    
    [Inject]
    public CarSpawner(GameConfig gameConfig, GameObjectFactory factory, CinemachineVirtualCamera virtualCamera,
        Transform playerSpawnPoint)
    {
        _gameConfig = gameConfig;
        _factory = factory;
        _virtualCamera = virtualCamera;
        _playerSpawnPoint = playerSpawnPoint;
    }

    public SimcadeVehicleController SpawnPlayer()
    {
        var playerCar = SpawnCar();
        
        _virtualCamera.Follow = playerCar.transform;
        _virtualCamera.LookAt = playerCar.transform;
        
        return playerCar;
    }

    public void SpawnGhost()
    {
        
    }

    private SimcadeVehicleController SpawnCar()
    {
        return  _factory.CreateWithResolving(_gameConfig.PlayerCar, _playerSpawnPoint.position);
    }
}