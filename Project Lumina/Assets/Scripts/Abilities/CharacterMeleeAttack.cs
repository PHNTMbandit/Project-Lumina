using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Melee Attack")]
    public class CharacterMeleeAttack : CharacterAbility
    {
        public int CurrentMeleeAttackIndex
        {
            get => _currentMeleeAttackIndex;
            set => _currentMeleeAttackIndex = value <= 0 ? 0 : value >= _attackCombos.Length - 1 ? _attackCombos.Length - 1 : value;
        }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        private int _currentMeleeAttackIndex = 0;
        private Attack _currentMeleeAttack;
        private Animator _animator;

        public UnityAction onComboFinished;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UseMeleeAttack()
        {
            _currentMeleeAttack = _attackCombos[CurrentMeleeAttackIndex];

            if (_currentMeleeAttack.IsUnlocked)
            {
                _animator.Play(_currentMeleeAttack.AttackAnimation.name);

                CurrentMeleeAttackIndex++;
            }

        }

        public void MeleeAttack()
        {
            foreach (Damageable damageable in _currentMeleeAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(_currentMeleeAttack.Damage);

                ObjectPoolController.Instance.GetPooledObject(_currentMeleeAttack.HitFX.name, damageable.transform.position, new Quaternion(0, transform.localScale.x, 0, 0), false);
            }
        }

        public void FinishMeleeAttackCombo()
        {
            CurrentMeleeAttackIndex = 0;

            onComboFinished?.Invoke();
        }

        public void ResetMeleeAttackCombo()
        {
            CurrentMeleeAttackIndex = 0;
        }
    }
}