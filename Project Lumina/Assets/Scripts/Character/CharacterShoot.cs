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
        private RangedAttack[] _attackCombos;

        [BoxGroup("Shooting"), SerializeField, Range(0, 10)]
        private float _fireRate;

        private float _nextFire;
        private int _currentShootIndex = 0;
        private Attack _currentShoot;

        public UnityAction onShootFinished;
        public UnityAction<GameObject> onHit;

        public void UseShoot()
        {
            IsShooting = true;

            _currentShoot = _attackCombos[CurrentShootIndex];

            if (_currentShoot.IsUnlocked)
            {
                CurrentShootIndex++;
            }
        }

        public void TryShoot()
        {
            foreach (Damageable damageable in _currentShoot.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                if (_currentShoot.TryAttack(gameObject, damageable))
                {
                    onHit?.Invoke(damageable.gameObject);
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

        public RangedAttack[] GetRangedAttacks()
        {
            return _attackCombos;
        }
    }
}