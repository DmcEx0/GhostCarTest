using System;
using Ashsvp;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

public class RaceController : IInitializable, IStartable, IDisposable
{
    private const int MaxRaceCount = 2;

    private readonly TimerBeforeStart _timerBeforeStart;
    private readonly PathRecorder _pathRecorder;
    private readonly Button _startButton;
    private readonly IInputRouter _inputRouter;
    private readonly FinishGate _finishGate;
    private readonly CarSpawner _carSpawner;

    private TMP_Text _raceCounterText;

    private int _currentRace = 1;

    public RaceController(Button startButton, TimerBeforeStart timerBeforeStart, PathRecorder pathRecorder,
        PlayerInputRouter playerInputRouter, FinishGate finishGate, CarSpawner carSpawner, TMP_Text raceCounterText)
    {
        _timerBeforeStart = timerBeforeStart;
        _pathRecorder = pathRecorder;
        _startButton = startButton;
        _inputRouter = playerInputRouter;
        _finishGate = finishGate;
        _carSpawner = carSpawner;
        _raceCounterText = raceCounterText;
    }

    public void Initialize()
    {
        _startButton.onClick.AddListener(StartRace);
        _finishGate.FinishReached += OnFinishReached;

        _inputRouter.OnDisable();
    }

    public void Start()
    {
        SetRaceCounterText();
        var playerCar = _carSpawner.SpawnPlayer();

        switch (_currentRace)
        {
            case 1:
                StartWithoutGhost(playerCar);
                break;
            case 2:
                StartWithGhost();
                break;
        }
    }

    public void Dispose()
    {
        _startButton.onClick.RemoveListener(StartRace);
        _finishGate.FinishReached -= OnFinishReached;
    }

    private void StartWithoutGhost(SimcadeVehicleController playerCar)
    {
        _pathRecorder.SetPlayerTransform(playerCar.transform);
    }

    private void StartWithGhost()
    {

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

        _currentRace = Mathf.Clamp(_currentRace++, 1, MaxRaceCount);
    }

    
    private void SetRaceCounterText()
    {
        _raceCounterText.text = string.Format(_raceCounterText.text, _currentRace);
    }
}
