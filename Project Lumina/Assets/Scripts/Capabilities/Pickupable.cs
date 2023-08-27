using ProjectLumina.Pickups;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [RequireComponent(typeof(Pickup))]
    public class Pickupable : MonoBehaviour
    {
        private Pickup _pickup;

        public UnityEvent onPickup;

        private void Awake()
        {
            _pickup = GetComponent<Pickup>();
        }

        public void Pickup(GameObject target)
        {
            _pickup.TryPickup(target);

            onPickup?.Invoke();
        }
    }
}