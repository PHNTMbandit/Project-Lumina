using System;
using System.Collections;
using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Aerial Attack")]
    public class CharacterAerialAttack : CharacterAbility
    {
        public bool IsAttacking { get; private set; }
        public bool IsSlowStop { get; private set; }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        [ToggleGroup("SlowStop")]
        public bool SlowStop;

        [ToggleGroup("SlowStop"), Range(0, 10), SerializeField]
        private float _slowStopGravityScale;

        [ToggleGroup("SlowStop"), Range(0, 10), SerializeField]
        private float _slowStopVelocityScale;

        [ToggleGroup("SlowStop"), SerializeField, Range(0, 5)]
        private float _slowStopTimer;

        private int _comboIndex = 0;
        private bool _canContinueCombo = true;
        private Attack _currentAerialAttack;
        private Animator _animator;
        private Rigidbody2D _rb;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        public void AerialAttack()
        {
            if (_canContinueCombo)
            {
                _comboIndex++;

                if (_comboIndex > _attackCombos.Length)
                {
                    _comboIndex = 1;
                }

                _currentAerialAttack = _attackCombos[_comboIndex - 1];
                _animator.SetTrigger($"aerial attack {_comboIndex}");

                _canContinueCombo = false;
                IsAttacking = true;
            }
        }

        public void AerialAttackDamage()
        {
            foreach (Damageable damageable in _currentAerialAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (damageable.IsDamageable)
                {
                    damageable.Damage(_currentAerialAttack.Damage);

                    if (damageable.TryGetComponent(out HitFX hitFX))
                    {
                        hitFX.ShowHitFX(_currentAerialAttack.HitFX.name);
                    }

                    if (damageable.TryGetComponent(out HitStop hitStop))
                    {
                        hitStop.Stop(_currentAerialAttack.HitStopDuration);
                    }

                    if (damageable.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(_currentAerialAttack.Damage, transform.position, _currentAerialAttack.Colour);
                    }

                    if (SlowStop)
                    {
                        StartCoroutine(StartSlowStop());
                    }
                }
            }
        }

        private IEnumerator StartSlowStop()
        {
            IsSlowStop = true;

            _rb.gravityScale = _slowStopGravityScale;
            _rb.velocity /= _slowStopVelocityScale;

            yield return new WaitForSeconds(_slowStopTimer);

            IsSlowStop = false;
        }

        public void ContinueAerialAttackCombo()
        {
            _canContinueCombo = true;
        }

        public void FinishAerialAttack()
        {
            _comboIndex = 0;
            _canContinueCombo = true;
            IsAttacking = false;
        }
    }
}