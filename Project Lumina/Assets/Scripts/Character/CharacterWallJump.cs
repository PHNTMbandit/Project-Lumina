using Micosmo.SensorToolkit;
using ProjectLumina.Player.Input;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Character/Character Wall Jump")]
    public class CharacterWallJump : CharacterAbility
    {
        [Range(0, 50), SerializeField]
        private float _wallJumpUpForce;

        [Range(0, 500), SerializeField]
        private float _wallJumpForce;

        [FoldoutGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        private Rigidbody2D _rb;
        private CharacterMove _characterMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _characterMove = GetComponent<CharacterMove>();
        }

        public bool CanWallJump()
        {
            GameObject wall = _sensor.GetNearestDetection();
            return wall != null;
        }

        public void WallJump()
        {
            _rb.AddForce(
                -_characterMove.GetFacingDirection() * _wallJumpForce,
                ForceMode2D.Impulse
            );
            _rb.AddForce(Vector2.up * _wallJumpUpForce, ForceMode2D.Impulse);
        }
    }
}
