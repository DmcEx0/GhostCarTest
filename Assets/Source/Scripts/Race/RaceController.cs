using System;
using Ashsvp;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class RaceController : IInitializable, IStartable, IDisposable
{
    private const int MaxRaceCount = 2;

    private readonly TimerBeforeStart _timerBeforeStart;
    private readonly IPathRecorder _pathRecorder;
    private readonly UIController _uiController;
    private readonly FinishGate _finishGate;
    private readonly CarSpawner _carSpawner;

    private SimcadeVehicleController _playerCar;
    private SimcadeVehicleController _ghostCar;

    private int _currentRace = 1;

    public RaceController(UIController uiController , TimerBeforeStart timerBeforeStart, IPathRecorder pathRecorder, 
        FinishGate finishGate, CarSpawner carSpawner)
    {
        _uiController = uiController;
        _timerBeforeStart = timerBeforeStart;
        _pathRecorder = pathRecorder;
        _finishGate = finishGate;
        _carSpawner = carSpawner;
    }

    public void Initialize()
    {
        _uiController.StartButton.onClick.AddListener(StartRace);
        _finishGate.FinishReached += OnFinishReached;
    }

    public void Start()
    {
        _uiController.NextRaceButton.gameObject.SetActive(false);
        _uiController.StartButton.gameObject.SetActive(true);

        SetRaceCounterText();
        _playerCar = _carSpawner.SpawnPlayer();
        _playerCar.SetInputRouterEnabledState(false);

        switch (_currentRace)
        {
            case 1:
                StartWithoutGhost();
                break;
            case 2:
                StartWithGhost();
                break;
        }
    }

    public void Dispose()
    {
        _uiController.StartButton.onClick.RemoveListener(StartRace);
        _finishGate.FinishReached -= OnFinishReached;
    }

    private void StartWithoutGhost()
    {
        _pathRecorder.SetPlayerTransform(_playerCar.transform);
    }

    private void StartWithGhost()
    {
        _ghostCar = _carSpawner.SpawnGhost();
        _ghostCar.SetInputRouterEnabledState(false);
    }

    private void StartRace()
    {
        _uiController.StartButton.gameObject.SetActive(false);

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
        _uiController.RaceCounterText.text = string.Format(_uiController.RaceCounterText.text, _currentRace);
    }

    private void OnFinishReached()
    {
        _uiController.NextRaceButton.gameObject.SetActive(true);
        _uiController.NextRaceButton.onClick.AddListener(OnStartNextRace);

        SwitchCarsInputEnabledState(false);

        _pathRecorder.StopRecordPath();

        _currentRace = Mathf.Clamp(_currentRace++, 1, MaxRaceCount);
    }
    
    private void OnStartNextRace()
    {
        Object.Destroy(_playerCar);
        
        if(_ghostCar !=null)
        {
            Object.Destroy(_ghostCar);
        }
        
        Start();
        
        _uiController.NextRaceButton.onClick.RemoveListener(OnStartNextRace);
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