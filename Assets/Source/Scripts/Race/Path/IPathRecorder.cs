using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IPathRecorder
{
    public UniTask StartRecordPathAsync();
    public void StopRecordPath();
    public void SetPlayerTransform(Transform playerTransform);
    public void AddPathPoint();
}