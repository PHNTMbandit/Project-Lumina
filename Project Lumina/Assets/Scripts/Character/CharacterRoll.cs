using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Roll")]
    public class CharacterRoll : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsRolling { get; private set; }

        [BoxGroup("Speed"), Range(0, 10), SerializeField]
        private float _rollSpeed;

        [BoxGroup("Collider"), SerializeField]
        private float _colliderSizeY, _colliderOffsetY;

        private float _defaultColliderOffsetY, _defaultColliderSizeY;
        private BoxCollider2D _collider;
        private CharacterMove _characterMove;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _characterMove = GetComponent<CharacterMove>();
            _rb = GetComponent<Rigidbody2D>();

            _defaultColliderSizeY = _collider.size.y;
            _defaultColliderOffsetY = _collider.offset.y;
        }

        public void Roll()
        {
            IsRolling = true;

            _collider.size = new Vector2(_collider.size.x, _colliderSizeY);
            _collider.offset = new Vector2(_collider.offset.x, _colliderOffsetY);
            _rb.velocity = _characterMove.GetFacingDirection() * _rollSpeed;
        }

        public void FinishRoll()
        {
            IsRolling = false;

            _collider.size = new Vector2(_collider.size.x, _defaultColliderSizeY);
            _collider.offset = new Vector2(_collider.offset.x, _defaultColliderOffsetY);
        }
    }
}