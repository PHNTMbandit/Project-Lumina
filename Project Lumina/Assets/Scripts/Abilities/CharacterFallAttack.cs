using System.Collections.Generic;
using Micosmo.SensorToolkit;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall Attack")]
    public class CharacterFallAttack : CharacterAbility
    {
        public bool IsFallAttacking { get; private set; }
        public bool FallAttackCharge { get; private set; } = true;

        [ToggleGroup("FallAttack"), SerializeField]
        private bool FallAttack;

        [ToggleGroup("FallAttack"), SerializeField]
        private Attack _fallAttack;

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

        public void UseFallAttack()
        {
            IsFallAttacking = true;
        }

        public void DealFallAttack()
        {
            foreach (Damageable damageable in _fallAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(_fallAttack.Damage);
            }
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

        public void SetGravityScale()
        {
            if (BulletTime)
            {
                _rb.velocity *= _bulletTimeMultiplier;
            }
        }
    }
}