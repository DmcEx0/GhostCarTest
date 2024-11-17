using Cysharp.Threading.Tasks;

namespace GhostRaceTest.Race
{
    public interface ITimerBeforeStart
    {
        public UniTask StartTimerAsync();

    }
}