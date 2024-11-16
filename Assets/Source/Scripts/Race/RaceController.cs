using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using VContainer.Unity;

public class RaceController : IInitializable, IDisposable
{
    private readonly TimerBeforeStart _timerBeforeStart;
    private readonly PathRecorder _pathRecorder;
    private readonly Button _startButton;
    private readonly IInputRouter _inputRouter;
    private readonly FinishGate _finishGate;

    public RaceController(Button startButton, TimerBeforeStart timerBeforeStart, PathRecorder pathRecorder,
        PlayerInputRouter playerInputRouter, FinishGate finishGate)
    {
        _timerBeforeStart = timerBeforeStart;
        _pathRecorder = pathRecorder;
        _startButton = startButton;
        _inputRouter = playerInputRouter;
        _finishGate = finishGate;
    }

    public void Initialize()
    {
        _startButton.onClick.AddListener(StartRace);
        _finishGate.FinishReached += OnFinishReached;
        _inputRouter.OnDisable();
    }
    
    public void Dispose()
    {
        _startButton.onClick.RemoveListener(StartRace);
        _finishGate.FinishReached -= OnFinishReached;
    }

    private void StartRace()
    {
        _startButton.gameObject.SetActive(false);
        
        StartRaceAsync().Forget();
        async UniTask StartRaceAsync()
        {
            await _timerBeforeStart.StartTimerAsync();
            
            _inputRouter.OnEnable();

            _pathRecorder.StartRecordPathAsync().Forget();
        }
    }
    
    private void OnFinishReached()
    {
        _inputRouter.OnDisable();
        _pathRecorder.StopRecordPath();
    }
}