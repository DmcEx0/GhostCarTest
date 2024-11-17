using GhostRaceTest.Ghost;
using UnityEngine;
using VContainer;

namespace GhostRaceTest.Input
{
    public class GhostInputRouter : IInputRouter, IGhostInputRouter
    {
        private readonly GhostAI _ghostAi;

        private Transform _ghostTransform;

        private bool _isEnabled;

        [Inject]
        public GhostInputRouter(GhostAI ghostAI)
        {
            _ghostAi = ghostAI;
        }

        public void OnEnable()
        {
            _isEnabled = true;
            _ghostAi.SetNextPoint();
        }

        public void OnDisable()
        {
            _isEnabled = false;
            _ghostAi.Reset();
        }

        public float GetAcceleration()
        {
            if (_isEnabled == false)
            {
                return 0;
            }

            return _ghostAi.CurrentSpeed;
        }

        public float GetSteering()
        {
            if (_isEnabled == false)
            {
                return 0;
            }

            return _ghostAi.GetDirectionToNextPoint(_ghostTransform);
        }

        public float GetBreak()
        {
            if (_isEnabled == false)
            {
                return 0;
            }

            return _ghostAi.BreakingValue;
        }

        public void SetTransform(Transform ghostTransform)
        {
            _ghostTransform = ghostTransform;
        }
    }
}