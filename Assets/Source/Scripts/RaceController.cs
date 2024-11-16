using Cysharp.Threading.Tasks;

public class RaceController
{
    private Timer _timer;
    private PathRecorder _pathRecorder;

    public RaceController(GameConfig gameConfig)
    {
        
    }
    
    private async UniTask StartRace()
    {
        await _timer.StartTimerAsync();
        
        _pathRecorder.StartRecordPathAsync();
    }
}