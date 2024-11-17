using VContainer;

public class GhostInputRouter : IInputRouter
{
    private GhostAI _ghostAi;
    private GameConfig _gameConfig;

    private bool _isEnabled;

    [Inject]
    public GhostInputRouter(GhostAI ghostAI, GameConfig gameConfig)
    {
        _ghostAi = ghostAI;
        _gameConfig = gameConfig;
    }
    
    public void OnEnable()
    {
        _isEnabled = true;
    }

    public void OnDisable()
    {
        _isEnabled = false;
    }

    public float GetAcceleration()
    {
        if (_isEnabled == false)
        {
            return 0;
        }
        
        return _gameConfig.GhostSpeed;
    }

    public float GetSteering()
    {
        if (_isEnabled == false)
        {
            return 0;
        }
        
        return 0;
    }

    public float GetBreak()
    {
        if (_isEnabled == false)
        {
            return 0;
        }
        
        return 0;
    }
}