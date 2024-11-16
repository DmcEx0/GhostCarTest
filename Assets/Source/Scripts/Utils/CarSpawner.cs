using Ashsvp;
using Cinemachine;
using UnityEngine;
using VContainer;

public class CarSpawner
{
    private readonly GameConfig _gameConfig;
    private readonly GameObjectFactory _factory;
    private readonly Transform _playerSpawnPoint;
    private readonly Transform _ghostSpawnPoint;
    private readonly CinemachineVirtualCamera _virtualCamera;
    private readonly PlayerInputRouter _playerInputRouter;
    private readonly GhostInputRouter _ghostInputRouter;
    
    [Inject]
    public CarSpawner(GameConfig gameConfig, GameObjectFactory factory, CinemachineVirtualCamera virtualCamera,
        Transform playerSpawnPoint, Transform ghostSpawnPoint, PlayerInputRouter playerInputRouter,
        GhostInputRouter ghostInputRouter)
    {
        _gameConfig = gameConfig;
        _factory = factory;
        _virtualCamera = virtualCamera;
        _playerSpawnPoint = playerSpawnPoint;
        _ghostSpawnPoint = ghostSpawnPoint;
    }

    public SimcadeVehicleController SpawnPlayer()
    {
        var playerCar = SpawnCar(_playerSpawnPoint);
        playerCar.Configure(_playerInputRouter);
        
        _virtualCamera.Follow = playerCar.transform;
        _virtualCamera.LookAt = playerCar.transform;
        
        return playerCar;
    }

    public SimcadeVehicleController SpawnGhost()
    {
        var ghostCar = SpawnCar(_ghostSpawnPoint);
        ghostCar.Configure(_ghostInputRouter);

        return ghostCar;
    }

    private SimcadeVehicleController SpawnCar(Transform spawnPoint)
    {
        return  _factory.Create(_gameConfig.PlayerCar, spawnPoint.position);
    }
}