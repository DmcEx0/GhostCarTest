using Cinemachine;
using GhostRaceTest.Configs;
using UnityEngine;
using VContainer;

namespace GhostRaceTest.Utils
{
    public class CameraSetter : ICameraSetter
    {
        [Inject] private CinemachineVirtualCamera _camera;
        [Inject] private GameConfig _gameConfig;
        
        public void SetCameraDefaultPosition()
        {
            _camera.gameObject.SetActive(false);
            _camera.transform.position = _gameConfig.CameraDefaultPosition;
            _camera.transform.rotation = Quaternion.identity;
        }
        
        public void SetCameraTarget(Transform target)
        {
            _camera.Follow = target;
            _camera.LookAt = target;
            
            _camera.gameObject.SetActive(true);
        }
    }
}