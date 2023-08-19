using System;
using System.Collections.Generic;
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
        public bool AerialAttackCharge { get; private set; } = true;
        public int CurrentAerialAttackIndex
        {
            get => _currentAerialAttackIndex;
            set => _currentAerialAttackIndex = value <= 0 ? 0 : value >= 2 ? 2 : value;
        }

        [ToggleGroup("AerialSlash1"), SerializeField]
        private bool AerialSlash1;

        [ToggleGroup("AerialSlash1"), SerializeField]
        private Attack AerialSlash1Attack;

        [ToggleGroup("AerialSlash2"), SerializeField]
        private bool AerialSlash2;

        [ToggleGroup("AerialSlash2"), SerializeField]
        private Attack AerialSlash2Attack;

        [ToggleGroup("BulletTime")]
        public bool BulletTime;

        [ToggleGroup("BulletTime"), Range(0, 1), SerializeField]
        private float _bulletTimeMultiplier;

        private int _currentAerialAttackIndex = 1;
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
            switch (CurrentAerialAttackIndex)
            {
                case 1:
                    if (AerialSlash1)
                    {
                        _animator.SetInteger("aerial attack", CurrentAerialAttackIndex);
                        CurrentAerialAttackIndex++;
                    }
                    break;

                case 2:
                    if (AerialSlash2)
                    {
                        _animator.SetInteger("aerial attack", CurrentAerialAttackIndex);
                        CurrentAerialAttackIndex++;
                    }
                    break;
            }
        }

        public void AerialAttack()
        {
            switch (CurrentAerialAttackIndex)
            {
                case 1:
                    Damage(AerialSlash1Attack);
                    break;

                case 2:
                    Damage(AerialSlash2Attack);
                    break;
            }
        }

        public void FinishAttack()
        {
            AerialAttackCharge = false;

            onComboFinished?.Invoke();
        }

        public void ResetAerialCombo()
        {
            AerialAttackCharge = true;

            CurrentAerialAttackIndex = 1;
        }

        public void SetGravityScale()
        {
            if (BulletTime)
            {
                _rb.velocity *= _bulletTimeMultiplier;
            }
        }

        private void Damage(Attack attack)
        {
            foreach (Damageable damageable in attack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(attack.Damage);
            }
        }
    }
}