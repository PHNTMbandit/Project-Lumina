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
    [AddComponentMenu("Character/Character Roll Attack")]
    public class CharacterRollAttack : CharacterAbility
    {
        public bool IsRollAttacking { get; private set; }

        [BoxGroup("Attack Combos"), SerializeField]
        private MeleeAttack[] _attackCombos;

        [BoxGroup("Settings"), Range(0, 10), SerializeField]
        private float _rollAttackSpeed;

        private int _comboIndex = 0;
        private bool _canContinueCombo = true;
        private Attack _currentRollAttack;
        private Animator _animator;
        private Rigidbody2D _rb;

        public UnityAction<GameObject> onHit;

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

        public void TryRollAttack()
        {
            foreach (Damageable damageable in _currentRollAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (_currentRollAttack.TryAttack(gameObject, damageable))
                {
                    onHit?.Invoke(damageable.gameObject);
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

        public MeleeAttack[] GetRollAttacks()
        {
            return _attackCombos;
        }
    }
}