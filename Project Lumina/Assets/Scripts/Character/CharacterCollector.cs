using Micosmo.SensorToolkit;
using ProjectLumina.Capabilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Collector")]
    public class CharacterCollector : CharacterAbility
    {
        [FoldoutGroup("References"), SerializeField]
        private RangeSensor2D _sensor;

        [Space]
        public UnityEvent onPickup;

        private void Awake()
        {
            _sensor.OnDetected.AddListener(Pickup);
        }

        private void Pickup(GameObject pickup, Sensor sensor)
        {
            if (pickup.TryGetComponent(out Pickupable pickupable))
            {
                pickupable.Pickup(gameObject);
            }

            onPickup?.Invoke();
        }
    }
}