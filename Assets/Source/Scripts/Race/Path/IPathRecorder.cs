using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IPathRecorder
{
    public UniTask StartRecordPathAsync();
    public void StopRecordPath();
    public void Reset();
    public void SetPlayerTransform(Transform playerTransform);
}