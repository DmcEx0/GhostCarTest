using System;
using Cysharp.Threading.Tasks;
using GhostRaceTest.Configs;
using GhostRaceTest.Race.Path;
using UnityEngine;
using VContainer;

namespace GhostRaceTest.Ghost
{
    public class GhostAI
    {
        private const float AngleOffset = 2f;
        private const float DefaultDirectionValue = 1f;
        private const float DefaultBreakingValue = 1f;
        private const float DefaultNotBreakingValue = 0f;

        private readonly IPathPoints _pathPoints;
        private readonly GhostConfig _ghostConfig;

        private Vector3 _nextPoint;
        private int _nextPointIndex;

        private bool _canBreaking;
        private float _currentAngle;

        public float CurrentSpeed => GetSpeed();
        public float BreakingValue { get; private set; }

        [Inject]
        public GhostAI(IPathPoints pathPoints, GhostConfig ghostConfig)
        {
            _pathPoints = pathPoints;
            _ghostConfig = ghostConfig;
            Reset();
        }

        public float GetDirectionToNextPoint(Transform ghostTransform)
        {
            if (IsNextPointReached(ghostTransform))
            {
                NextPointReached();
            }

            Vector3 directionToTarget = _nextPoint - ghostTransform.position;
            directionToTarget.y = 0;

            Vector3 playerForward = ghostTransform.forward;

            _currentAngle = Vector3.SignedAngle(playerForward, directionToTarget, Vector3.up);

            if (_currentAngle > AngleOffset)
            {
                return DefaultDirectionValue;
            }

            if (_currentAngle < -AngleOffset)
            {
                return -DefaultDirectionValue;
            }

            return 0;
        }

        public void SetNextPoint()
        {
            _nextPoint = GetNextPoint(_nextPointIndex);
        }

        private float GetSpeed()
        {
            var absoluteAngle = Mathf.Abs(_currentAngle);

            if (absoluteAngle >= _ghostConfig.AngleForBreaking)
            {
                PreformBreakingAsync().Forget();
                return _ghostConfig.SpeedOnSteering;
            }

            return _ghostConfig.MaxSpeed;
        }

        public void Reset()
        {
            _nextPointIndex = 0;
            _currentAngle = 0f;
            _canBreaking = true;
        }

        private async UniTask PreformBreakingAsync()
        {
            if (_canBreaking == false)
            {
                return;
            }

            _canBreaking = false;
            BreakingValue = DefaultBreakingValue;

            await UniTask.Delay(TimeSpan.FromMilliseconds(_ghostConfig.MillisecondsBreakingTime));

            BreakingValue = DefaultNotBreakingValue;
        }

        private void NextPointReached()
        {
            _nextPointIndex++;
            SetNextPoint();
            _canBreaking = true;
        }

        private bool IsNextPointReached(Transform ghostTransform)
        {
            return Vector3.Distance(ghostTransform.position, _nextPoint) < _ghostConfig.DistanceForRegisterPoint;
        }

        private Vector3 GetNextPoint(int index)
        {
            if (index == _pathPoints.PathPoints.Count)
            {
                return Vector3.zero;
            }

            return _pathPoints.PathPoints[index];
        }
    }
}