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
    [RequireComponent(typeof(HitStop))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall Attack")]
    public class CharacterFallAttack : CharacterAbility
    {
        public bool IsFallAttacking { get; private set; }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        private int _comboIndex = 0;
        private bool _canContinueCombo;
        private Attack _currentFallAttack;
        private Animator _animator;
        private HitStop _hitStop;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _hitStop = GetComponent<HitStop>();
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

        public void FallAttackDamage()
        {
            foreach (Damageable damageable in _currentFallAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(_currentFallAttack.Damage);

                _hitStop.Stop(_currentFallAttack.HitStopDuration);

                ObjectPoolController.Instance.GetPooledObject(_currentFallAttack.HitFX.name, damageable.transform.position, new Quaternion(transform.localScale.x, 0, 0, 0), false);
                ObjectPoolController.Instance.GetPooledObject("Damage Indicator", damageable.transform.position, ObjectPoolController.Instance.transform, true)
                                             .GetComponent<DamageIndicator>()
                                             .ShowIndicator(_currentFallAttack.Damage.ToString(), transform.position, damageable.transform.position);
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
    }
}