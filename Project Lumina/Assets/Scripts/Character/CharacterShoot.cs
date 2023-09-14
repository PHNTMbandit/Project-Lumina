using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Shoot")]
    public class CharacterShoot : CharacterAbility
    {
        public bool IsShooting { get; private set; }

        public int CurrentShootIndex
        {
            get => _currentShootIndex;
            set => _currentShootIndex = value <= 0 ? 0 : value >= _attackCombos.Length - 1 ? _attackCombos.Length - 1 : value;
        }

        [BoxGroup("Attack Combos"), SerializeField]
        private Attack[] _attackCombos;

        [BoxGroup("Shooting"), SerializeField, Range(0, 10)]
        private float _fireRate;

        private float _nextFire;
        private int _currentShootIndex = 0;
        private Attack _currentShoot;
        private Animator _animator;

        public UnityAction onShootFinished;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UseShoot()
        {
            IsShooting = true;

            _currentShoot = _attackCombos[CurrentShootIndex];

            if (_currentShoot.IsUnlocked)
            {
                _animator.Play(_currentShoot.AttackAnimation.name);

                CurrentShootIndex++;
            }
        }

        public void Shoot()
        {
            foreach (Damageable damageable in _currentShoot.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (damageable.IsDamageable)
                {
                    damageable.Damage(_currentShoot.Damage);

                    if (damageable.TryGetComponent(out HitFX hitFX))
                    {
                        hitFX.ShowHitFX(_currentShoot.HitFX.name);
                    }

                    if (damageable.TryGetComponent(out HitStop hitStop))
                    {
                        hitStop.Stop(_currentShoot.HitStopDuration);
                    }

                    if (damageable.TryGetComponent(out DamageIndicator damageIndicator))
                    {
                        damageIndicator.ShowDamageIndicator(_currentShoot.Damage, transform.position);
                    }
                }
            }
        }

        public void FinishShoot()
        {
            IsShooting = false;

            CurrentShootIndex = 0;

            onShootFinished?.Invoke();
        }

        public void ResetShooatCombo()
        {
            CurrentShootIndex = 0;
        }

        public bool CanShoot()
        {
            if (Time.time >= _nextFire)
            {
                _nextFire = Time.time + _fireRate;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}