using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Sprint")]
    public class CharacterSprint : MonoBehaviour
    {
        [BoxGroup("Sprint"), SerializeField, Range(0, 25)]
        private float _moveSpeed, _velocity, _acceleration;

        [BoxGroup("Stop"), SerializeField, Range(0, 25)]
        private float _decceleration, _frictionAmount;

        private float _lastMoveX, _moveInput;
        private Animator _animator;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_moveInput != 0)
            {
                _lastMoveX = _moveInput;
            }
        }

        public void Sprint(float move)
        {
            float targetSpeed = move * _moveSpeed;
            float speedDiff = targetSpeed - _rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, _velocity) * Mathf.Sign(speedDiff);

            _moveInput = move;
            _animator.SetFloat("speed", Mathf.Abs(_moveInput));

            if (_lastMoveX < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            _rb.AddForce(movement * Vector2.right);

            if (Mathf.Abs(move) < 0.01f)
            {
                float amount = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(_frictionAmount));
                amount *= Mathf.Sign(_rb.velocity.x);
                _rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
            }
        }
    }
}