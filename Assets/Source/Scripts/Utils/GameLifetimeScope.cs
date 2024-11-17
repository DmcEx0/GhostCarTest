using Cinemachine;
using GhostRaceTest.Configs;
using GhostRaceTest.Ghost;
using GhostRaceTest.Input;
using GhostRaceTest.Race;
using GhostRaceTest.Race.Path;
using GhostRaceTest.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GhostRaceTest.Utils
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private GhostConfig _ghostConfig;
        [SerializeField] private CinemachineVirtualCamera _camera;
    
        [Header("UI")]
        [SerializeField] private UIProvider _uiProvider;

        [Space] [Header("Track")] 
        [SerializeField] private FinishGate _finishGate;
        [SerializeField] private Transform _playerSpawnPoint;
    
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_gameConfig);
            builder.RegisterComponent(_ghostConfig);
            builder.RegisterComponent(_finishGate);
            builder.RegisterComponent(_uiProvider);

            builder.Register<TimerBeforeStart>(Lifetime.Scoped);
            builder.Register<CarSpawner>(Lifetime.Scoped).WithParameter(_camera).WithParameter(_playerSpawnPoint);
        
            builder.Register<PathRecorder>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<CameraSetter>(Lifetime.Scoped).WithParameter(_camera).AsImplementedInterfaces();
            builder.Register<PlayerInputRouter>(Lifetime.Scoped);
            builder.Register<GhostInputRouter>(Lifetime.Scoped).As<IGhostInputRouter>().AsSelf();
            builder.Register<GhostAI>(Lifetime.Scoped);
            builder.Register<GameObjectFactory>(Lifetime.Scoped);
        
            builder.RegisterEntryPoint<RaceController>().WithParameter(_playerSpawnPoint);
        }
    }
}