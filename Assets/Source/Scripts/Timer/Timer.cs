using Cysharp.Threading.Tasks;

public class Timer
{
    private const int MinTime = 0;
    
    private GameConfig _gameConfig;
    private int _currentTime;
    
    public Timer(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }
    
    public async UniTask StartTimer()
    {
        while (_currentTime >= MinTime)
        {
            
        }
    }
}