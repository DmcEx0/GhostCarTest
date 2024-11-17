using UnityEngine;

namespace GhostRaceTest.Configs
{
    [CreateAssetMenu(menuName = "Configs/GhostConfig", fileName = "GhostConfig")]
    public class GhostConfig : ScriptableObject
    {
        [field: SerializeField] public Material Material { get; private set; }
        
        [field: Range(0f, 10f)]
        [field: SerializeField] public float DistanceForRegisterPoint { get; private set; }
        
        [field: Range(10f, 40f)]
        [field: SerializeField] public float AngleForBreaking { get; private set; }
        
        [field: Range(0.1f, 1f)]
        [field: SerializeField] public float MaxSpeed { get; private set; }
        
        [field: Range(0.01f, 1f)]
        [field: SerializeField] public float SpeedOnSteering { get; private set; }
        
        [field: Range(0, 1000)]
        [field: SerializeField] public int MillisecondsBreakingTime { get; private set; }
    }
}