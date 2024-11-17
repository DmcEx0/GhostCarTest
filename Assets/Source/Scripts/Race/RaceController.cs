using System;
using Ashsvp;
using Cysharp.Threading.Tasks;
using GhostRaceTest.Race.Path;
using GhostRaceTest.UI;
using GhostRaceTest.Utils;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace GhostRaceTest.Race
{
    public class RaceController : IInitializable, IStartable, IDisposable
    {
        private const int FirstRace = 1;
        private const int SecondRace = 2;

        private readonly TimerBeforeStart _timerBeforeStart;
        private readonly IPathRecorder _pathRecorder;
        private readonly UIProvider _uiProvider;
        private readonly FinishGate _finishGate;
        private readonly CarSpawner _carSpawner;
        private readonly ICameraSetter _cameraSetter;

        private SimcadeVehicleController _playerCar;
        private SimcadeVehicleController _ghostCar;

        private int _currentRace = 1;

        public RaceController(UIProvider uiProvider, TimerBeforeStart timerBeforeStart, IPathRecorder pathRecorder,
            FinishGate finishGate, CarSpawner carSpawner, ICameraSetter cameraSetter)
        {
            _uiProvider = uiProvider;
            _timerBeforeStart = timerBeforeStart;
            _pathRecorder = pathRecorder;
            _finishGate = finishGate;
            _carSpawner = carSpawner;
            _cameraSetter = cameraSetter;
        }

        public void Initialize()
        {
            _uiProvider.StartButton.onClick.AddListener(StartRace);
            _uiProvider.NextRaceButton.onClick.AddListener(OnStartNextRace);
            _finishGate.FinishReached += OnFinishReached;
        }

        public void Dispose()
        {
            _uiProvider.StartButton.onClick.RemoveListener(StartRace);
            _uiProvider.NextRaceButton.onClick.RemoveListener(OnStartNextRace);
            _finishGate.FinishReached -= OnFinishReached;
        }

        public void Start()
        {
            _pathRecorder.StopRecordPath();

            _uiProvider.NextRaceButton.gameObject.SetActive(false);
            _uiProvider.StartButton.gameObject.SetActive(true);

            SetRaceCounterText();

            _playerCar = _carSpawner.SpawnPlayer();
            _playerCar.SetInputRouterEnabledState(false);

            switch (_currentRace)
            {
                case FirstRace:
                    StartWithoutGhost();
                    break;
                case SecondRace:
                    StartWithGhost();
                    break;
            }
        }

        private void StartWithoutGhost()
        {
            _pathRecorder.SetPlayerTransform(_playerCar.transform);
            _pathRecorder.StartRecordPathAsync().Forget();
        }

        private void StartWithGhost()
        {
            _ghostCar = _carSpawner.SpawnGhost();
            _ghostCar.SetInputRouterEnabledState(false);
        }

        private void StartRace()
        {
            _uiProvider.StartButton.gameObject.SetActive(false);

            StartRaceAsync().Forget();

            async UniTask StartRaceAsync()
            {
                await _timerBeforeStart.StartTimerAsync();

                SwitchCarsInputEnabledState(true);
            }
        }

        private void SetRaceCounterText()
        {
            _uiProvider.RaceCounterText.text = _currentRace.ToString();
        }

        private void OnFinishReached()
        {
            if (_currentRace == FirstRace)
            {
                _pathRecorder.AddPathPoint();
            }

            _uiProvider.NextRaceButton.gameObject.SetActive(true);

            SwitchCarsInputEnabledState(false);

            _pathRecorder.StopRecordPath();

            _currentRace += 1;
        }

        private void OnStartNextRace()
        {
            Restart();

            Start();
        }

        private void Restart()
        {
            if (_currentRace > SecondRace)
            {
                _currentRace = FirstRace;

                _pathRecorder.Reset();
            }

            Object.Destroy(_playerCar.gameObject);

            if (_ghostCar != null)
            {
                Object.Destroy(_ghostCar.gameObject);
            }
            
            _cameraSetter.SetCameraPosition();
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
}