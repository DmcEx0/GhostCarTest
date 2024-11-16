using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

public class RaceController : IInitializable, IStartable, IDisposable
{
    private readonly TimerBeforeStart _timerBeforeStart;
    private readonly PathRecorder _pathRecorder;
    private readonly Button _startButton;
    private readonly IInputRouter _inputRouter;
    private readonly FinishGate _finishGate;
    private readonly CarSpawner _carSpawner;

    public RaceController(Button startButton, TimerBeforeStart timerBeforeStart, PathRecorder pathRecorder,
        PlayerInputRouter playerInputRouter, FinishGate finishGate, CarSpawner carSpawner)
    {
        _timerBeforeStart = timerBeforeStart;
        _pathRecorder = pathRecorder;
        _startButton = startButton;
        _inputRouter = playerInputRouter;
        _finishGate = finishGate;
        _carSpawner = carSpawner;
    }

    public void Initialize()
    {
        _startButton.onClick.AddListener(StartRace);
        _finishGate.FinishReached += OnFinishReached;

        _inputRouter.OnDisable();
    }

    public void Start()
    {
        var playerCar = _carSpawner.SpawnPlayer();
        _pathRecorder.SetPlayerTransform(playerCar.transform);
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