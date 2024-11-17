using System;
using Ashsvp;
using UnityEngine;

namespace GhostRaceTest.Race
{
    public class FinishGate : MonoBehaviour
    {
        public event Action FinishReached;
    
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out SimcadeVehicleController carController))
            {
                FinishReached?.Invoke();
            }
        }
    }
}