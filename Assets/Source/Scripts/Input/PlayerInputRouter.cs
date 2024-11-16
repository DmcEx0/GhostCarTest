public class PlayerInputRouter : IInputRouter
{
    private UserInput _userInput;

    public PlayerInputRouter()
    {
        _userInput = new UserInput();
    }

    public void OnEnable()
    {
        _userInput.Enable();
    }
    
    public void OnDisable()
    {
        _userInput.Disable();
    }

    public float GetAcceleration()
    {
        return _userInput.Player.Acceleration.ReadValue<float>();
    }
    
    public float GetSteering()
    {
        return _userInput.Player.Steering.ReadValue<float>();
    }
    
    public float GetBreak()
    {
        return _userInput.Player.Break.ReadValue<float>();
    }
}