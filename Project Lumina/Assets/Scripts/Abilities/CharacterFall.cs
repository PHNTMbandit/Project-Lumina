using System.Collections;
using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Abilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall")]
    public class CharacterFall : CharacterAbility
    {
        [BoxGroup("Thresholds"), SerializeField, Range(-10, 0)]
        private float _fallingThreshold;

        [BoxGroup("Thresholds"), SerializeField, Range(-100, 0)]
        private float _maxFallGravitySpeed;

        [BoxGroup("Thresholds"), SerializeField, Range(0, 10)]
        private float _fallingGravityScale, _fallingGravityMultiplier;

        [BoxGroup("Sensors"), SerializeField]
        private RaySensor2D _sensor;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetGravityScale()
        {
            _rb.gravityScale = _fallingGravityScale * _fallingGravityMultiplier;
            _rb.velocity = new(_rb.velocity.x, Mathf.Max(_rb.velocity.y, _maxFallGravitySpeed));
        }

        public bool IsFalling()
        {
            if (_rb.velocity.y < _fallingThreshold)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CanFallThrough()
        {
            return _sensor.GetDetections().Exists(i => i.layer == LayerMask.NameToLayer("Platform"));
        }

        public IEnumerator FallThrough()
        {
            var platform = _sensor.GetNearestComponent<PlatformEffector2D>();

            _sensor.DetectsOnLayers &= ~(1 << LayerMask.NameToLayer("Platform"));
            platform.colliderMask &= ~(1 << LayerMask.NameToLayer("Player"));

            yield return new WaitForSeconds(0.35f);

            _sensor.DetectsOnLayers |= 1 << LayerMask.NameToLayer("Platform");
            platform.colliderMask |= 1 << LayerMask.NameToLayer("Player");
        }
    }
}