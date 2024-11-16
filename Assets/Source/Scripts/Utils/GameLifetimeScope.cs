using Ashsvp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameConfig _gameConfig;
    
    [Header("UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _raceCounterText;
    [SerializeField] private TMP_Text _timerText;

    [Space] [Header("Car")] 
    [SerializeField] private SimcadeVehicleController _carController;
    
    [Space] [Header("Track")] 
    [SerializeField] private FinishGate _finishGate;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_gameConfig);
        builder.RegisterComponent(_carController);
        builder.RegisterComponent(_finishGate);

        builder.Register<TimerBeforeStart>(Lifetime.Scoped).WithParameter(_timerText);
        builder.Register<PathRecorder>(Lifetime.Scoped).WithParameter(_carController.transform);
        builder.Register<PlayerInputRouter>(Lifetime.Scoped);
        builder.Register<GameObjectFactory>(Lifetime.Scoped);
        
        builder.RegisterEntryPoint<RaceController>().WithParameter(_startButton);
    }
}
