using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Melee Attack")]
    public class CharacterMeleeAttack : CharacterAbility
    {
        public bool IsAttacking { get; private set; }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        private int _comboIndex = 0;
        private bool _canContinueCombo = true;
        private Attack _currentMeleeAttack;
        private Animator _animator;

        public UnityAction<GameObject> onHit;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void MeleeAttack()
        {
            if (_canContinueCombo)
            {
                _comboIndex++;
                _currentMeleeAttack = _attackCombos[_comboIndex - 1];

                if (_currentMeleeAttack.IsUnlocked)
                {
                    if (_comboIndex > _attackCombos.Length)
                    {
                        _comboIndex = 1;
                    }

                    _animator.SetTrigger($"melee attack {_comboIndex}");

                    _canContinueCombo = false;
                    IsAttacking = true;
                }
                else
                {
                    _comboIndex = 1;
                }
            }
        }

        public void MeleeAttackDamage()
        {
            foreach (Damageable damageable in _currentMeleeAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (damageable.IsDamageable)
                {
                    damageable.Damage(_currentMeleeAttack.Damage);

                    if (damageable.TryGetComponent(out HitFX hitFX))
                    {
                        hitFX.ShowHitFX(_currentMeleeAttack.HitFX.name);
                    }

                    if (damageable.TryGetComponent(out HitStop hitStop))
                    {
                        hitStop.Stop(_currentMeleeAttack.HitStopDuration);
                    }

                    if (damageable.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(_currentMeleeAttack.Damage, transform.position, _currentMeleeAttack.Colour);
                    }

                    onHit?.Invoke(damageable.gameObject);
                }
            }
        }

        public void ContinueMeleeAttackCombo()
        {
            _canContinueCombo = true;
        }

        public void FinishMeleeAttack()
        {
            _comboIndex = 0;
            _canContinueCombo = true;
            IsAttacking = false;
        }
    }
}