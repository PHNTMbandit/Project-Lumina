using Micosmo.SensorToolkit;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Capabilities
{
    [AddComponentMenu("Capabilities/Pickupable")]
    public class Pickupable : MonoBehaviour
    {
        [SerializeField]
        private RangeSensor2D _sensor;

        [Space]
        public UnityEvent<GameObject> onPickup;

        private void Awake()
        {
            _sensor.OnDetected.AddListener(Pickup);
        }

        private void Pickup(GameObject target, Sensor sensor)
        {
            onPickup?.Invoke(target);
        }
    }
}