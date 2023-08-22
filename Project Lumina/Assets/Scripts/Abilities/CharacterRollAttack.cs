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
    [AddComponentMenu("Character/Character Roll Attack")]
    public class CharacterRollAttack : CharacterAbility
    {
        public int CurrentRollAttackIndex
        {
            get => _currentRollAttackIndex;
            set => _currentRollAttackIndex = value <= 0 ? 0 : value >= _attackCombos.Length - 1 ? _attackCombos.Length - 1 : value;
        }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        private int _currentRollAttackIndex = 0;
        private Attack _currentRollAttack;
        private Animator _animator;

        public UnityAction onRollAttackFinished;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UseRollAttack()
        {
            _currentRollAttack = _attackCombos[CurrentRollAttackIndex];

            if (_currentRollAttack.IsUnlocked)
            {
                _animator.Play(_currentRollAttack.AttackAnimation.name);

                CurrentRollAttackIndex++;
            }
        }

        public void RollAttack()
        {
            foreach (Damageable damageable in _currentRollAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(_currentRollAttack.Damage);

                ObjectPoolController.Instance.GetPooledObject(_currentRollAttack.HitFX.name, damageable.transform.position, new Quaternion(transform.localScale.x, 0, 0, 0), false);
            }
        }

        public void FinishRollAttack()
        {
            CurrentRollAttackIndex = 0;

            onRollAttackFinished?.Invoke();
        }

        public void ResetRollAttackCombo()
        {
            CurrentRollAttackIndex = 0;
        }
    }
}