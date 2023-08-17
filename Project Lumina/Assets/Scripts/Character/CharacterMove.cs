using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Move")]
    public class CharacterMove : MonoBehaviour
    {
        [BoxGroup("Move"), SerializeField, Range(0, 25)]
        private float _moveSpeed, _velocity, _acceleration;

        [BoxGroup("Stop"), SerializeField, Range(0, 25)]
        private float _decceleration, _frictionAmount;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Move(float move)
        {
            float targetSpeed = move * _moveSpeed;
            float speedDiff = targetSpeed - _rb.velocity.x;
            float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _acceleration : _decceleration;
            float movement = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, _velocity) * Mathf.Sign(speedDiff);

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