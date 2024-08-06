using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [AddComponentMenu("Character/Character Wall Slide")]
    public class CharacterWallSlide : CharacterAbility
    {
        [Range(0, 10), SerializeField]
        private float _wallSlideSpeed;

        [FoldoutGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        private Rigidbody2D _rb;
        private CharacterMove _characterMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _characterMove = GetComponent<CharacterMove>();
        }

        public bool CanWallSlide()
        {
            GameObject wall = _sensor.GetNearestDetection();
            return wall != null;
        }

        public void Slide()
        {
            _rb.velocity = new Vector2(0, -_wallSlideSpeed);
        }
    }
}
