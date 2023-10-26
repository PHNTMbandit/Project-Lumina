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
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall Attack")]
    public class CharacterFallAttack : CharacterAbility
    {
        public bool IsFallAttacking { get; private set; }

        [BoxGroup("Attack Combos"), SerializeField]
        private MeleeAttack[] _attackCombos;

        private int _comboIndex = 0;
        private bool _canContinueCombo;
        private Attack _currentFallAttack;
        private Animator _animator;

        public UnityAction<GameObject> onHit;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void FallAttack()
        {
            if (_canContinueCombo)
            {
                _comboIndex++;

                if (_comboIndex > _attackCombos.Length)
                {
                    _comboIndex = 1;
                }

                _currentFallAttack = _attackCombos[_comboIndex - 1];
                _animator.SetTrigger($"fall attack {_comboIndex}");

                _canContinueCombo = false;
                IsFallAttacking = true;
            }
        }

        public void TryFallAttack()
        {
            foreach (Damageable damageable in _currentFallAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (_currentFallAttack.TryAttack(gameObject, damageable))
                {
                    onHit?.Invoke(damageable.gameObject);
                }
            }
        }

        public void ContinueFallAttackCombo()
        {
            _canContinueCombo = true;
        }

        public void FinishFallAttack()
        {
            _comboIndex = 0;
            _canContinueCombo = true;
            IsFallAttacking = false;
        }

        public MeleeAttack[] GetFallAttacks()
        {
            return _attackCombos;
        }
    }
}