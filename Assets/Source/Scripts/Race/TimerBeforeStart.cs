using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using GhostRaceTest.Configs;
using GhostRaceTest.UI;

namespace GhostRaceTest.Race
{
    public class TimerBeforeStart : ITimerBeforeStart
    {
        private const int MinTime = 1;
        private const int TimerStep = 1;

        private readonly GameConfig _gameConfig;
        private readonly UIProvider _uiProvider;
    
        private TimeSpan _currentTime;
    
        public TimerBeforeStart(UIProvider uiProvider, GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
            _uiProvider = uiProvider;
        }
    
        public async UniTask StartTimerAsync()
        {
            _currentTime = TimeSpan.FromSeconds(_gameConfig.SecondsToStart);
            _uiProvider.TimerText.gameObject.SetActive(true);
        
            while (_currentTime >= TimeSpan.FromSeconds(MinTime))
            {
                _uiProvider.TimerText.text = _currentTime.TotalSeconds.ToString(CultureInfo.CurrentCulture);
            
                await UniTask.Delay(TimeSpan.FromSeconds(TimerStep));
            
                _currentTime -= TimeSpan.FromSeconds(TimerStep);
            }
        
            _uiProvider.TimerText.gameObject.SetActive(false);
        }
    }
}