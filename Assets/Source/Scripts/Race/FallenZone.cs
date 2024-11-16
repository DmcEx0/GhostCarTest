using System;
using Ashsvp;
using UnityEngine;

public class FallenZone : MonoBehaviour
{
    public event Action Fallen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SimcadeVehicleController carController))
        {
            Fallen?.Invoke();
        }
    }
}