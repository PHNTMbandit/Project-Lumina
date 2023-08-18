using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Dash")]
    public class CharacterDash : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsDashing { get; private set; }

        [BoxGroup("Speed"), Range(0, 50), SerializeField]
        private float _dashSpeed;

        private Rigidbody2D _rb;
        private CharacterMove _characterMove;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _characterMove = GetComponent<CharacterMove>();
        }

        public void Dash()
        {
            IsDashing = true;

            _rb.velocity = new Vector2(_characterMove.GetFacingDirection().x * _dashSpeed, 0);
            _rb.gravityScale = 0;
        }

        public void FinishDash()
        {
            IsDashing = false;
        }
    }
}