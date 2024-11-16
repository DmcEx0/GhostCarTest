using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public int SecondsToStart { get; set; }
    [field: SerializeField] public int MillisecondsToRegisterPathPoint { get; set; }
    [field: SerializeField] public int MillisecondsToStartRegisterPathPoint { get; set; }
}