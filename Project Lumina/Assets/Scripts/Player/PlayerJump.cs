using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Player/Player Jump")]
    public class PlayerJump : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsGrounded { get; private set; }

        [SerializeField, Range(0, 5)]
        private float _jumpForce;

        [SerializeField]
        private RaySensor2D _sensor;

        private float _lastGroundedTime, _lastJumpedTime;
        private Rigidbody2D _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            _sensor.OnDetected.AddListener(delegate { IsGrounded = true; });
            _sensor.OnLostDetection.AddListener(delegate { IsGrounded = false; });
        }

        public void Jump()
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _lastGroundedTime = 0;
            _lastJumpedTime = 0;
        }
    }
}