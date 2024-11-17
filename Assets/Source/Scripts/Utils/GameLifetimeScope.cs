using Cinemachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private CinemachineVirtualCamera _camera;
    
    [Header("UI")]
    [SerializeField] private UIController _uiController;

    [Space] [Header("Track")] 
    [SerializeField] private FinishGate _finishGate;
    [SerializeField] private Transform _playerSpawnPoint;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_gameConfig);
        builder.RegisterComponent(_finishGate);
        builder.RegisterComponent(_uiController);

        builder.Register<TimerBeforeStart>(Lifetime.Scoped);
        builder.Register<CarSpawner>(Lifetime.Scoped).WithParameter(_camera).WithParameter(_playerSpawnPoint);
        
        builder.Register<PathRecorder>(Lifetime.Scoped).AsImplementedInterfaces();
        builder.Register<PlayerInputRouter>(Lifetime.Scoped);
        builder.Register<GhostInputRouter>(Lifetime.Scoped).As<IGhostInputRouter>().AsSelf();
        builder.Register<GhostAI>(Lifetime.Scoped);
        builder.Register<GameObjectFactory>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<RaceController>().WithParameter(_playerSpawnPoint);
    }
}