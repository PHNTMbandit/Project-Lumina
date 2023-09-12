using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
using ProjectLumina.UI;
using Sirenix.OdinInspector;
using UnityEngine;

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

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void MeleeAttack()
        {
            if (_canContinueCombo)
            {
                _comboIndex++;

                if (_comboIndex > _attackCombos.Length)
                {
                    _comboIndex = 1;
                }

                _currentMeleeAttack = _attackCombos[_comboIndex - 1];
                _animator.SetTrigger($"melee attack {_comboIndex}");

                _canContinueCombo = false;
                IsAttacking = true;
            }
        }

        public void MeleeAttackDamage()
        {
            foreach (Damageable damageable in _currentMeleeAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.HitStop(_currentMeleeAttack.HitStopDuration);
                damageable.ShowDamageIndicator(_currentMeleeAttack.Damage, transform.position);
                damageable.ShowHitFX(_currentMeleeAttack.HitFX.name);
                damageable.Damage(_currentMeleeAttack.Damage);
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