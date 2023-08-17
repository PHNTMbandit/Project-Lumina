using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall")]
    public class CharacterFall : MonoBehaviour
    {
        public bool IsFalling { get; private set; }

        [field: BoxGroup("Thresholds"), SerializeField, Range(-10, 0)]
        public float FallingThreshold { get; private set; }

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_rb.velocity.y < FallingThreshold)
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