using Ashsvp;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig")]
public class GameConfig : ScriptableObject
{
    [field: SerializeField] public GameObject TrackPointPrefab { get; private set; }
    [field: SerializeField] public SimcadeVehicleController PlayerCar { get; private set; }
    [field: SerializeField] public int SecondsToStart { get; private set; }
    [field: SerializeField] public float DistanceBetweenPathPoints { get; private set; }
    [field: SerializeField] public bool ShowPathPoints { get; private set; }
}