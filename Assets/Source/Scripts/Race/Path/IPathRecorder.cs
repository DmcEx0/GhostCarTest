using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GhostRaceTest.Race.Path
{
    public interface IPathRecorder
    {
        public UniTask StartRecordPathAsync();
        public void StopRecordPath();
        public void SetPlayerTransform(Transform playerTransform);
        public void AddPathPoint();
        public void Reset();
    }
}