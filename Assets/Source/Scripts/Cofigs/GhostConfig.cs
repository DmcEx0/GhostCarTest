using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GhostConfig", fileName = "GhostConfig")]
public class GhostConfig : ScriptableObject
{
    [field: SerializeField] public Material Material { get; private set; }
    [field: SerializeField] public float DistanceForRegisterPoint { get; private set; }
    [field: SerializeField] public float AngleForBreaking { get; private set; }
    [field: SerializeField] public float MaxSpeed { get; private set; }
    [field: SerializeField] public float SpeedOnSteering { get; private set; }
    [field: SerializeField] public int MillisecondsBreakingTime { get; private set; }
}