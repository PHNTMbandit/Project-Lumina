using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall Attack")]
    public class CharacterFallAttack : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsFallAttacking { get; private set; }

        [field: BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool FallAttackCharge { get; private set; } = true;

        [ToggleGroup("BulletTime")]
        public bool BulletTime;

        [ToggleGroup("BulletTime"), Range(0, 1), SerializeField]
        private float _bulletTimeMultiplier;

        private Rigidbody2D _rb;

        public UnityAction onFallAttackFinished;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        public void SetGravityScale()
        {
            if (BulletTime)
            {
                _rb.velocity *= _bulletTimeMultiplier;
            }
        }

        public void FallAttack()
        {
            IsFallAttacking = true;
        }

        public void FinishFallAttack()
        {
            IsFallAttacking = false;
            FallAttackCharge = false;

            onFallAttackFinished?.Invoke();
        }

        public void ResetFallAttack()
        {
            FallAttackCharge = true;
        }
    }
}