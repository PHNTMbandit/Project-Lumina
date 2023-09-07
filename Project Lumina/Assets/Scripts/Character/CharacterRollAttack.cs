using System.Collections;
using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
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
                _rb.velocity = new Vector2(0, 0);

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
                damageable.Damage(_currentRollAttack.Damage);

                ObjectPoolController.Instance.GetPooledObject(_currentRollAttack.HitFX.name, damageable.transform.position, new Quaternion(transform.localScale.x, 0, 0, 0), false);
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