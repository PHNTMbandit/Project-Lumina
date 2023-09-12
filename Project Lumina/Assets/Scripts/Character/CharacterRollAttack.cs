using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
using ProjectLumina.Effects;
using ProjectLumina.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(HitStop))]
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
        private HitStop _hitStop;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _hitStop = GetComponent<HitStop>();
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
                damageable.Damage(_currentRollAttack.Damage);

                _hitStop.Stop(_currentRollAttack.HitStopDuration);

                ObjectPoolController.Instance.GetPooledObject(_currentRollAttack.HitFX.name, damageable.transform.position, new Quaternion(transform.localScale.x, 0, 0, 0), false);
                ObjectPoolController.Instance.GetPooledObject("Damage Indicator", damageable.transform.position, ObjectPoolController.Instance.transform, true)
                                             .GetComponent<DamageIndicator>()
                                             .ShowIndicator(_currentRollAttack.Damage.ToString(), transform.position, damageable.transform.position);
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