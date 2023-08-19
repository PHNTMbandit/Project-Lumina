using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Character/Character Wall Slide")]
    public class CharacterWallSlide : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool CanWallSlide { get; private set; }

        [ToggleGroup("WallSlide")]
        public bool WallSlide;

        [ToggleGroup("WallSlide"), Range(0, 10), SerializeField]
        private float _wallSlideSpeed;

        [ToggleGroup("WallJump")]
        public bool WallJump;

        [ToggleGroup("WallJump"), Range(0, 50), SerializeField]
        private float _wallJumpUpForce;

        [ToggleGroup("WallJump"), Range(0, 100), SerializeField]
        private float _wallJumpForce;

        [FoldoutGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        private Rigidbody2D _rb;
        private CharacterMove _characterMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _characterMove = GetComponent<CharacterMove>();

            _sensor.OnDetected.AddListener(delegate { CanWallSlide = true; });
            _sensor.OnLostDetection.AddListener(delegate { CanWallSlide = false; });
        }

        public void Jump()
        {
            CanWallSlide = false;

            _rb.AddForce(-_characterMove.GetFacingDirection() * _wallJumpForce, ForceMode2D.Impulse);
            _rb.AddForce(Vector2.up * _wallJumpUpForce, ForceMode2D.Impulse);
        }

        public void Slide()
        {
            _rb.velocity = new Vector2(0, -_wallSlideSpeed);
        }
    }
}