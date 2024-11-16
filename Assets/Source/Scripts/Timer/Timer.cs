using System;
using System.Globalization;
using Cysharp.Threading.Tasks;
using TMPro;

public class Timer
{
    private const int MinTime = 1;
    private const int TimerStep = 1;

    private readonly GameConfig _gameConfig;
    private readonly TMP_Text _timerText;
    
    private TimeSpan _currentTime;
    
    public Timer(TMP_Text timerText, GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
        _timerText = timerText;
    }
    
    public async UniTask StartTimerAsync()
    {
        _currentTime = TimeSpan.FromSeconds(_gameConfig.SecondsToStart);
        _timerText.gameObject.SetActive(true);
        
        while (_currentTime >= TimeSpan.FromSeconds(MinTime))
        {
            _timerText.text = _currentTime.TotalSeconds.ToString(CultureInfo.CurrentCulture);
            
            await UniTask.Delay(TimeSpan.FromSeconds(TimerStep));
            
            _currentTime -= TimeSpan.FromSeconds(TimerStep);
        }
        
        _timerText.gameObject.SetActive(false);
    }
}