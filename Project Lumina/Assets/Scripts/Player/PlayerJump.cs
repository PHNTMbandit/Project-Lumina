using Micosmo.SensorToolkit;
using UnityEngine;

namespace ProjectLumina.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Player/Player Jump")]
    public class PlayerJump : MonoBehaviour
    {
        [SerializeField, Range(0, 25)]
        private float _jumpForce;

        [SerializeField]
        private RaySensor2D _sensor;

        private float _lastGroundedTime, _lastJumpedTime;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public bool CanJump()
        {
            if (_lastGroundedTime > 0 && _lastJumpedTime > 0)
            {
                if (_sensor.GetNearestDetection() != null)
                {
                    return true;
                }
            }

            return false;
        }

        public void Jump()
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _lastGroundedTime = 0;
            _lastJumpedTime = 0;
        }
    }
}