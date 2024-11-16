using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameConfig _gameConfig;
    
    [Header("UI")]
    [SerializeField] private Button _startButton;
    [SerializeField] private TMP_Text _raceCounterText;
    [SerializeField] private TMP_Text _timerText;
    
    protected override void Configure(IContainerBuilder builder)
    {
    }
}
