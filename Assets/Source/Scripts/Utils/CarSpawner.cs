using Ashsvp;
using GhostRaceTest.Configs;
using GhostRaceTest.Input;
using UnityEngine;
using VContainer;

namespace GhostRaceTest.Utils
{
    public class CarSpawner
    {
        private readonly GameConfig _gameConfig;
        private readonly GhostConfig _ghostConfig;
        private readonly GameObjectFactory _factory;
        private readonly ICameraSetter _cameraSetter;
        
        private readonly Transform _playerSpawnPoint;
        private readonly Transform _ghostSpawnPoint;
        
        private readonly IInputRouter _playerInputRouter;
        private readonly IInputRouter _ghostInputRouter;
        private readonly IGhostInputRouter _iGhostInputRouter;
        
        [Inject]
        public CarSpawner(GameConfig gameConfig, GameObjectFactory factory, ICameraSetter cameraSetter,
            Transform playerSpawnPoint, Transform ghostSpawnPoint, PlayerInputRouter playerInputRouter,
            GhostInputRouter ghostInputRouter, GhostConfig ghostConfig)
        {
            _gameConfig = gameConfig;
            _factory = factory;
            _cameraSetter = cameraSetter;
            _playerSpawnPoint = playerSpawnPoint;
            _ghostSpawnPoint = ghostSpawnPoint;
            _playerInputRouter = playerInputRouter;
            _ghostInputRouter = ghostInputRouter;
            _iGhostInputRouter = ghostInputRouter;
            _ghostConfig = ghostConfig;
        }
    
        public SimcadeVehicleController SpawnPlayer()
        {
            var playerCar = SpawnCar(_playerSpawnPoint);
            playerCar.Configure(_playerInputRouter);
            
            _cameraSetter.SetCameraDefaultPosition();
            _cameraSetter.SetCameraTarget(playerCar.transform);
            
            return playerCar;
        }
    
        public SimcadeVehicleController SpawnGhost()
        {
            var ghostCar = SpawnCar(_ghostSpawnPoint);
            ghostCar.Configure(_ghostInputRouter);
            ghostCar.BodyMesh.material = _ghostConfig.Material;
            _iGhostInputRouter.SetTransform(ghostCar.transform);
    
            return ghostCar;
        }
    
        private SimcadeVehicleController SpawnCar(Transform spawnPoint)
        {
            return  _factory.Create(_gameConfig.PlayerCar, spawnPoint.position);
        }
    }
}