using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using TMPro;

public class TimerBeforeStart
{
    private const int MinTime = 1;
    private const int TimerStep = 1;

    private readonly GameConfig _gameConfig;
    private readonly UIController _uiController;
    
    private TimeSpan _currentTime;
    
    public TimerBeforeStart(UIController uiController, GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
        _uiController = uiController;
    }
    
    public async UniTask StartTimerAsync()
    {
        _currentTime = TimeSpan.FromSeconds(_gameConfig.SecondsToStart);
        _uiController.TimerText.gameObject.SetActive(true);
        
        while (_currentTime >= TimeSpan.FromSeconds(MinTime))
        {
            _uiController.TimerText.text = _currentTime.TotalSeconds.ToString(CultureInfo.CurrentCulture);
            
            await UniTask.Delay(TimeSpan.FromSeconds(TimerStep));
            
            _currentTime -= TimeSpan.FromSeconds(TimerStep);
        }
        
        _uiController.TimerText.gameObject.SetActive(false);
    }
}