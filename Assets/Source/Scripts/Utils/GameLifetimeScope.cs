using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private CinemachineVirtualCamera _camera;
    
    [Header("UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _raceCounterText;
    [SerializeField] private TMP_Text _timerText;

    [Space] [Header("Track")] 
    [SerializeField] private FinishGate _finishGate;
    [SerializeField] private FallenZone _fallenZone;
    [SerializeField] private Transform _playerSpawnPoint;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_gameConfig);
        builder.RegisterComponent(_finishGate);
        builder.RegisterComponent(_fallenZone);

        builder.Register<TimerBeforeStart>(Lifetime.Scoped).WithParameter(_timerText);
        builder.Register<CarSpawner>(Lifetime.Scoped).WithParameter(_camera).WithParameter(_playerSpawnPoint);
        
        builder.Register<PathRecorder>(Lifetime.Scoped).AsImplementedInterfaces();
        builder.Register<PlayerInputRouter>(Lifetime.Scoped);
        builder.Register<GhostInputRouter>(Lifetime.Scoped);
        builder.Register<GhostAI>(Lifetime.Scoped);
        builder.Register<GameObjectFactory>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<RaceController>().WithParameter(_startButton).WithParameter(_playerSpawnPoint)
            .WithParameter(_raceCounterText);
    }
}
