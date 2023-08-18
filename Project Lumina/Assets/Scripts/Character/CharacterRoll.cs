using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Roll")]
    public class CharacterRoll : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsRolling { get; private set; }

        [BoxGroup("Speed"), Range(0, 10), SerializeField]
        private float _rollSpeed;

        private CharacterMove _characterMove;
        private Rigidbody2D _rb;

        public UnityAction onRollFinished;

        private void Awake()
        {
            _characterMove = GetComponent<CharacterMove>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Roll()
        {
            IsRolling = true;

            _rb.AddForce(_characterMove.GetFacingDirection() * _rollSpeed, ForceMode2D.Impulse);
        }

        public void FinishRoll()
        {
            IsRolling = false;
        }
    }
}