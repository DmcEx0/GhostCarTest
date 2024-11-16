using UnityEngine;

public interface IInputRouter
{
    public void OnEnable();
    public void OnDisable();
    public float GetAcceleration();
    public float GetSteering();
    public float GetBreak();
}