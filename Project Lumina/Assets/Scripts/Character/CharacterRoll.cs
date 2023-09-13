using ProjectLumina.Capabilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Damageable))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Roll")]
    public class CharacterRoll : CharacterAbility
    {
        public bool IsRolling { get; private set; }

        [ToggleGroup("Roll"), SerializeField]
        private bool Roll;

        [ToggleGroup("Roll"), SerializeField]
        private CapsuleCollider2D _collider;

        [ToggleGroup("Roll"), Range(0, 10), SerializeField]
        private float _rollSpeed;

        [ToggleGroup("Roll"), SerializeField]
        private float _colliderSizeY, _colliderOffsetY;

        private float _defaultColliderOffsetY, _defaultColliderSizeY;
        private Damageable _damageable;
        private CharacterMove _characterMove;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _damageable = GetComponent<Damageable>();
            _characterMove = GetComponent<CharacterMove>();
            _rb = GetComponent<Rigidbody2D>();

            _defaultColliderSizeY = _collider.size.y;
            _defaultColliderOffsetY = _collider.offset.y;
        }


        public void RollCharacter()
        {
            IsRolling = true;

            _damageable.SetDamageable(false);

            _collider.size = new Vector2(_collider.size.x, _colliderSizeY);
            _collider.offset = new Vector2(_collider.offset.x, _colliderOffsetY);
            _rb.velocity = _characterMove.GetFacingDirection() * _rollSpeed;
        }

        public void FinishRoll()
        {
            IsRolling = false;

            _damageable.SetDamageable(true);

            _collider.size = new Vector2(_collider.size.x, _defaultColliderSizeY);
            _collider.offset = new Vector2(_collider.offset.x, _defaultColliderOffsetY);
        }
    }
}