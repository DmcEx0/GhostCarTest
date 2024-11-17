using UnityEngine;

namespace GhostRaceTest.Utils
{
    public interface ICameraSetter
    {
        public void SetCameraDefaultPosition();
        public void SetCameraTarget(Transform target);
    }
}