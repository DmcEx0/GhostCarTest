using Cinemachine;
using UnityEngine;
using VContainer;

namespace GhostRaceTest.Utils
{
    public class CameraSetter : ICameraSetter
    {
        [Inject] private CinemachineVirtualCamera _camera;
        
        public void SetCameraPosition()
        {
            _camera.transform.position = Vector3.zero;
            _camera.transform.rotation = Quaternion.identity;
        }
    }
}