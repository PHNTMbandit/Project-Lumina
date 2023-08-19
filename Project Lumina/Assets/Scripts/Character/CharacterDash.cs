using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Dash")]
    public class CharacterDash : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public int CurrentDashCharges
        {
            get => _currentDashCharges;
            set => _currentDashCharges = value <= 0 ? 0 : value >= _dashCharges ? _dashCharges : value;
        }

        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsDashing { get; private set; }

        [BoxGroup("Dash"), Range(0, 10), SerializeField]
        private int _dashCharges;

        [BoxGroup("Dash"), Range(0, 50), SerializeField]
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
            _currentDashCharges = _dashCharges;
        }

        public void Dash()
        {
            if (IsDashing == false)
            {
                CurrentDashCharges--;
            }

            IsDashing = true;

            _rb.velocity = new Vector2(_characterMove.GetFacingDirection().x * _dashSpeed, 0);
            _rb.gravityScale = 0;
        }

        public void FinishDash()
        {
            IsDashing = false;
        }

        public void ResetDash()
        {
            CurrentDashCharges = _dashCharges;
        }
    }
}