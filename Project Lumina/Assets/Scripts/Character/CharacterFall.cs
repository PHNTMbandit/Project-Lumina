using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall")]
    public class CharacterFall : MonoBehaviour
    {
        [BoxGroup("Thresholds"), SerializeField, Range(-10, 0)]
        private float _fallingThreshold;

        [BoxGroup("Thresholds"), SerializeField, Range(-100, 0)]
        private float _maxFallGravitySpeed;

        [BoxGroup("Thresholds"), SerializeField, Range(0, 10)]
        private float _fallingGravityScale, _fallingGravityMultiplier;

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
    }
}