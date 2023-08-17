using Micosmo.SensorToolkit;
using ProjectLumina.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Jump")]
    public class CharacterJump : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsGrounded { get; private set; }

        [BoxGroup("Force"), SerializeField, Range(0, 25)]

        private float _jumpForce;

        [BoxGroup("Quality of Life"), SerializeField, Range(0, 10)]
        private float _jumpCutMultiplier;

        [BoxGroup("Quality of Life"), SerializeField, Range(0, 1)]
        private float _jumpCoyoteTime;

        [BoxGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        [BoxGroup("References"), SerializeField]
        private InputReader _inputReader;

        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        private float _lastGroundedTime;

        private Rigidbody2D _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _sensor.OnDetected.AddListener(delegate { IsGrounded = true; });
            _sensor.OnLostDetection.AddListener(delegate { IsGrounded = false; });
        }

        private void Update()
        {
            if (IsGrounded)
            {

                _lastGroundedTime = _jumpCoyoteTime;
            }
            else
            {
                _lastGroundedTime -= Time.deltaTime;
            }

            if (_rb.velocity.y > 0 && _inputReader.JumpInputRelease)
            {
                _rb.AddForce(_rb.velocity.y * (1 - _jumpCutMultiplier) * Vector2.down, ForceMode2D.Impulse);
            }
        }

        public bool CanJump()
        {
            if (_lastGroundedTime > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Jump()
        {
            _lastGroundedTime = 0f;

            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
