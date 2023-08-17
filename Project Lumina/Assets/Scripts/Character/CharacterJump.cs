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

        [BoxGroup("Times"), SerializeField, Range(0, 1)]
        private float _jumpBufferTime, _jumpCoyoteTime;

        [BoxGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        [BoxGroup("References"), SerializeField]
        private InputReader _inputReader;

        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        private float _lastGroundedTime, _lastJumpedTime;

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

            if (_inputReader.JumpInput)
            {
                _lastJumpedTime = _jumpBufferTime;
            }
            else
            {
                _lastJumpedTime -= Time.deltaTime;
            }
        }

        public bool CanJump()
        {
            if (_lastGroundedTime > 0f && _lastJumpedTime > 0f)
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
            Debug.Log("jump");

            _lastGroundedTime = 0f;
            _lastJumpedTime = 0f;

            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
