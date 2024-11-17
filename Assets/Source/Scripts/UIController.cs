using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [field: SerializeField] public Button StartButton { get; private set; }
    [field: SerializeField] public Button NextRaceButton { get; private set; }
    [field: SerializeField] public TMP_Text RaceCounterText { get; private set; }
    [field: SerializeField] public TMP_Text TimerText { get; private set; }
}