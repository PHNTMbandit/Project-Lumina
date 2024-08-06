using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Dash")]
    public class CharacterDash : CharacterAbility
    {
        public int CurrentDashCharges
        {
            get => _currentDashCharges;
            set =>
                _currentDashCharges =
                    value <= 0
                        ? 0
                        : value >= _dashCharges
                            ? _dashCharges
                            : value;
        }

        [Range(0, 10), SerializeField]
        private int _dashCharges;

        [Range(0, 50), SerializeField]
        private float _dashSpeed;

        private int _currentDashCharges;
        private Rigidbody2D _rb;
        private CharacterMove _characterMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _characterMove = GetComponent<CharacterMove>();
        }

        private void Start()
        {
            RechargeDash();
        }

        public void Dash()
        {
            if (CurrentDashCharges > 0)
            {
                CurrentDashCharges--;
                _rb.velocity = new Vector2(_characterMove.GetFacingDirection().x * _dashSpeed, 0);
                _rb.gravityScale = 0;
            }
        }

        public void RechargeDash()
        {
            CurrentDashCharges = _dashCharges;
        }
    }
}
