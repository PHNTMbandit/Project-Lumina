using System;
using System.Collections;
using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [RequireComponent(typeof(CharacterMove))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Aerial Attack")]
    public class CharacterAerialAttack : CharacterAbility
    {
        public bool IsSlowStop { get; private set; }

        public int CurrentAerialAttackIndex
        {
            get => _currentAerialAttackIndex;
            set => _currentAerialAttackIndex = value <= 0 ? 0 : value >= _attackCombos.Length - 1 ? _attackCombos.Length - 1 : value;
        }

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

                ObjectPoolController.Instance.GetPooledObject(_currentAerialAttack.HitFX.name, damageable.transform.position, new Quaternion(transform.localScale.x, 0, 0, 0), false);

                if (SlowStop)
                {
                    StartCoroutine(StartSlowStop());
                }
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

        private IEnumerator StartSlowStop()
        {
            IsSlowStop = true;

            _rb.gravityScale = _slowStopGravityScale;
            _rb.velocity /= _slowStopVelocityScale;

            yield return new WaitForSeconds(_slowStopTimer);

            IsSlowStop = false;
        }
    }
}