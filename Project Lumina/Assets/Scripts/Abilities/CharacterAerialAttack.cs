using System;
using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Aerial Attack")]
    public class CharacterAerialAttack : CharacterAbility
    {
        public int CurrentAerialAttackIndex
        {
            get => _currentAerialAttackIndex;
            set => _currentAerialAttackIndex = value <= 0 ? 0 : value >= _attackCombos.Length - 1 ? _attackCombos.Length - 1 : value;
        }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        private int _currentAerialAttackIndex = 0;
        private Attack _currentAerialAttack;
        private Rigidbody2D _rb;
        private Animator _animator;

        public UnityAction onComboFinished;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void UseAerialAttack()
        {
            _currentAerialAttack = _attackCombos[CurrentAerialAttackIndex];

            if (_currentAerialAttack.IsUnlocked)
            {
                _animator.Play(_currentAerialAttack.AttackAnimation.name);

                CurrentAerialAttackIndex++;
            }
        }

        public void AerialAttack()
        {
            foreach (Damageable damageable in _currentAerialAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(_currentAerialAttack.Damage);

                ObjectPoolController.Instance.GetPooledObject(_currentAerialAttack.HitFX.name, damageable.transform.position, false);
            }
        }

        public void FinishAerialAttackCombo()
        {
            CurrentAerialAttackIndex = 0;

            onComboFinished?.Invoke();
        }

        public void ResetAerialAttackCombo()
        {
            CurrentAerialAttackIndex = 0;
        }
    }
}