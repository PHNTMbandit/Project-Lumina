using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall")]
    public class CharacterFall : MonoBehaviour
    {
        public bool IsFalling { get; private set; }

        [BoxGroup("Thresholds"), SerializeField, Range(-10, 0)]
        private float _fallingThreshold;

        [BoxGroup("Thresholds"), SerializeField, Range(0, 10)]
        private float _fallingGravityScale, _fallingGravityMultiplier;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_rb.velocity.y < 0)
            {
                _rb.gravityScale = _fallingGravityScale * _fallingGravityMultiplier;
            }
            else
            {
                _rb.gravityScale = _fallingGravityScale;
            }

            if (_rb.velocity.y < _fallingThreshold)
            {
                IsFalling = true;
            }
            else
            {
                IsFalling = false;
            }
        }
    }
}