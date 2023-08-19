using System;
using System.Collections.Generic;
using Micosmo.SensorToolkit;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Aerial Attack")]
    public class CharacterAerialAttack : MonoBehaviour
    {
        [field: BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool AerialAttackCharge { get; private set; } = true;

        [field: BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public bool IsAerialAttacking { get; private set; } = true;

        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public int CurrentAerialComboIndex
        {
            get => _currentComboIndex;
            set => _currentComboIndex = value <= 0 ? 0 : value >= _attackCombos.Length ? _attackCombos.Length : value;
        }

        [BoxGroup("Combo Animations"), SerializeField]
        private AttackCombo[] _attackCombos;

        [ToggleGroup("BulletTime")]
        public bool BulletTime;

        [ToggleGroup("BulletTime"), Range(0, 1), SerializeField]
        private float _bulletTimeMultiplier;

        [FoldoutGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        private int _currentComboIndex;
        private Rigidbody2D _rb;
        private Animator _animator;

        public UnityAction onComboFinished;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        public void SetGravityScale()
        {
            if (BulletTime)
            {
                _rb.velocity *= _bulletTimeMultiplier;
            }
        }

        public void AerialAttack()
        {
            _attackCombos[CurrentAerialComboIndex - 1].SetRayAttackDistance(_sensor);

            foreach (Damageable damageable in _sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage();
            }
        }

        public void UseAerialCombo()
        {
            CurrentAerialComboIndex++;

            _animator.SetInteger("aerial combo", Array.IndexOf(_attackCombos, _attackCombos[CurrentAerialComboIndex - 1]) + 1);
        }

        public void FinishCombo()
        {
            AerialAttackCharge = false;

            onComboFinished?.Invoke();
        }

        public void ResetAerialCombo()
        {
            AerialAttackCharge = true;

            CurrentAerialComboIndex = 0;
            _sensor.Length = 0;
            _animator.SetInteger("aerial combo", 0);
        }
    }
}