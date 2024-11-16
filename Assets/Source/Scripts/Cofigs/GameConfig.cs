using Ashsvp;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public GameObject TrackPointPrefab { get; set; }
    [field: SerializeField] public SimcadeVehicleController PlayerCar { get; set; }
    [field: SerializeField] public int SecondsToStart { get; set; }
    [field: SerializeField] public int MillisecondsToRegisterPathPoint { get; set; }
    [field: SerializeField] public int MillisecondsToStartRegisterPathPoint { get; set; }
}