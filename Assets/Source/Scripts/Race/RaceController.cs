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
    private readonly IPathRecorder _pathRecorder;
    private readonly Button _startButton;
    private readonly FinishGate _finishGate;
    private readonly CarSpawner _carSpawner;
    private readonly FallenZone _fallenZone;

    private readonly TMP_Text _raceCounterText;
    
    private SimcadeVehicleController _playerCar;
    private SimcadeVehicleController _ghostCar;

    private int _currentRace = 1;

    public RaceController(Button startButton, TimerBeforeStart timerBeforeStart, IPathRecorder pathRecorder,
        PlayerInputRouter playerInputRouter, GhostInputRouter ghostInputRouter, FinishGate finishGate, CarSpawner carSpawner,
        TMP_Text raceCounterText, FallenZone fallenZone)
    {
        _timerBeforeStart = timerBeforeStart;
        _pathRecorder = pathRecorder;
        _startButton = startButton;
        _finishGate = finishGate;
        _carSpawner = carSpawner;
        _raceCounterText = raceCounterText;
        _fallenZone = fallenZone;
    }

    public void Initialize()
    {
        _startButton.onClick.AddListener(StartRace);
        _finishGate.FinishReached += OnFinishReached;
        _fallenZone.Fallen += OnFallen;
    }

    public void Start()
    {
        SetRaceCounterText();
        _playerCar = _carSpawner.SpawnPlayer();

        // switch (_currentRace)
        // {
        //     case 1:
        //         StartWithoutGhost(playerCar);
        //         break;
        //     case 2:
        //         StartWithGhost();
        //         break;
        // }
        StartWithoutGhost();
        StartWithGhost();
    }

    public void Dispose()
    {
        _startButton.onClick.RemoveListener(StartRace);
        _finishGate.FinishReached -= OnFinishReached;
        _fallenZone.Fallen -= OnFallen;
    }

    private void StartWithoutGhost()
    {
        _pathRecorder.SetPlayerTransform(_playerCar.transform);
        _playerCar.SetInputRouterEnabledState(false);
    }

    private void StartWithGhost()
    {
        _ghostCar = _carSpawner.SpawnGhost();
        _ghostCar.SetInputRouterEnabledState(false);
    }

    private void StartRace()
    {
        _startButton.gameObject.SetActive(false);

        StartRaceAsync().Forget();

        async UniTask StartRaceAsync()
        {
            await _timerBeforeStart.StartTimerAsync();

            SwitchCarsInputEnabledState(true);

            _pathRecorder.StartRecordPathAsync().Forget();
        }
    }

    private void SetRaceCounterText()
    {
        _raceCounterText.text = string.Format(_raceCounterText.text, _currentRace);
    }

    private void OnFinishReached()
    {
        SwitchCarsInputEnabledState(false);
        
        _pathRecorder.StopRecordPath();

        _currentRace = Mathf.Clamp(_currentRace++, 1, MaxRaceCount);
    }

    private void OnFallen()
    {
    }
    
    private void SwitchCarsInputEnabledState(bool state)
    {
        _playerCar.SetInputRouterEnabledState(state);

        if (_ghostCar != null)
        {
            _ghostCar.SetInputRouterEnabledState(state);
        }
    }
}