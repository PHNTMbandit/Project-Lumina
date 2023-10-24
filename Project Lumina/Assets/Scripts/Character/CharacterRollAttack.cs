using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Roll Attack")]
    public class CharacterRollAttack : CharacterAbility
    {
        public bool IsRollAttacking { get; private set; }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        [BoxGroup("Settings"), Range(0, 10), SerializeField]
        private float _rollAttackSpeed;

        private int _comboIndex = 0;
        private bool _canContinueCombo = true;
        private Attack _currentRollAttack;
        private Animator _animator;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void RollAttack()
        {
            if (_canContinueCombo)
            {
                _rb.velocity /= _rollAttackSpeed;

                _comboIndex++;

                if (_comboIndex > _attackCombos.Length)
                {
                    _comboIndex = 1;
                }

                _currentRollAttack = _attackCombos[_comboIndex - 1];
                _animator.SetTrigger($"roll attack {_comboIndex}");

                _canContinueCombo = false;
                IsRollAttacking = true;
            }
        }

        public void RollAttackDamage()
        {
            foreach (Damageable damageable in _currentRollAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (damageable.IsDamageable)
                {
                    damageable.Damage(_currentRollAttack.Damage);

                    if (damageable.TryGetComponent(out HitFX hitFX))
                    {
                        hitFX.ShowHitFX(_currentRollAttack.HitFX.name);
                    }

                    if (damageable.TryGetComponent(out HitStop hitStop))
                    {
                        hitStop.Stop(_currentRollAttack.HitStopDuration);
                    }

                    if (damageable.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(_currentRollAttack.Damage, transform.position, _currentRollAttack.Colour);
                    }

                    if (damageable.TryGetComponent(out CameraShake cameraShake))
                    {
                        cameraShake.Shake(_currentRollAttack.Damage);
                    }
                }
            }
        }

        public void ContinueRollAttackCombo()
        {
            _canContinueCombo = true;
        }

        public void FinishRollAttack()
        {
            _comboIndex = 0;
            _canContinueCombo = true;
            IsRollAttacking = false;
        }
    }
}