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
        public bool IsGrounded { get; private set; }

        [ToggleGroup("AddForce"), SerializeField]
        private bool AddForce;

        [ToggleGroup("AddForce"), SerializeField, Range(0, 25)]
        private float _jumpForce;

        [ToggleGroup("AddForce"), SerializeField, Range(0, 10)]
        private float _jumpGravityScale;

        [ToggleGroup("JumpCut"), SerializeField]
        private bool JumpCut;

        [ToggleGroup("JumpCut"), SerializeField, Range(0, 10)]
        private float _jumpCutMultiplier;

        [ToggleGroup("CoyoteTime"), SerializeField]
        private bool CoyoteTime;

        [ToggleGroup("CoyoteTime"), SerializeField, Range(0, 1)]
        private float _jumpCoyoteTime;

        [ToggleGroup("HangTime"), SerializeField]
        private bool HangTime;

        [ToggleGroup("HangTime"), Range(0, 10), SerializeField]
        private float _jumpHangGravityMult, _jumpHangTimeThreshold, _jumpHangAccelerationMultiplier, _jumpHangMaxSpeedMultiplier;

        [FoldoutGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        [FoldoutGroup("References"), SerializeField]
        private InputReader _inputReader;

        private float _lastGroundedTime;
        private float _accelerationRate;
        private float _targetSpeed;
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

            if (IsGrounded)
            {
                _rb.gravityScale = _jumpGravityScale;
            }
        }

        public void SetGravityScale()
        {
            if (JumpCut)
            {
                if (_rb.velocity.y > 0 && _inputReader.JumpInputRelease)
                {
                    _rb.AddForce(_rb.velocity.y * (1 - _jumpCutMultiplier) * Vector2.down, ForceMode2D.Impulse);
                }
            }

            if (HangTime)
            {
                if (Mathf.Abs(_rb.velocity.y) < _jumpHangTimeThreshold)
                {
                    _rb.gravityScale = _jumpGravityScale * _jumpHangGravityMult;

                    _accelerationRate = _jumpHangAccelerationMultiplier;
                    _targetSpeed *= _jumpHangMaxSpeedMultiplier;
                }
            }
        }

        public bool CanJump()
        {
            if (CoyoteTime)
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
            else
            {
                return IsGrounded;
            }
        }

        public void Jump()
        {
            if (AddForce)
            {
                _lastGroundedTime = 0f;

                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
