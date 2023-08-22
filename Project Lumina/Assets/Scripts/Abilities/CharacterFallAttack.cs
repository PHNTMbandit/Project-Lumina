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
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall Attack")]
    public class CharacterFallAttack : CharacterAbility
    {
        public int CurrentFallAttackIndex
        {
            get => _currentFallAttackIndex;
            set => _currentFallAttackIndex = value <= 0 ? 0 : value >= _attackCombos.Length - 1 ? _attackCombos.Length - 1 : value;
        }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        private int _currentFallAttackIndex = 0;
        private Attack _currentFallAttack;
        private Rigidbody2D _rb;
        private Animator _animator;

        public UnityAction onFallAttackFinished;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void UseFallAttack()
        {
            _currentFallAttack = _attackCombos[CurrentFallAttackIndex];

            if (_currentFallAttack.IsUnlocked)
            {
                _animator.Play(_currentFallAttack.AttackAnimation.name);

                CurrentFallAttackIndex++;
            }
        }

        public void FallAttack()
        {
            foreach (Damageable damageable in _currentFallAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(_currentFallAttack.Damage);

                ObjectPoolController.Instance.GetPooledObject(_currentFallAttack.HitFX.name, damageable.transform.position, new Quaternion(transform.localScale.x, 0, 0, 0), false);
            }
        }

        public void FinishFallAttack()
        {
            CurrentFallAttackIndex = 0;

            onFallAttackFinished?.Invoke();
        }

        public void ResetFallAttackCombo()
        {
            CurrentFallAttackIndex = 0;
        }
    }
}