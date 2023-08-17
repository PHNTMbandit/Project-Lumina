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
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Melee Attack")]
    public class CharacterMeleeAttack : MonoBehaviour
    {
        [BoxGroup("Stats"), ShowInInspector, ReadOnly]
        public int CurrentComboIndex
        {
            get => _currentComboIndex;
            set => _currentComboIndex = value <= 0 ? 0 : value >= _attackCombos.Length ? _attackCombos.Length : value;
        }

        [BoxGroup("Combo Animations"), SerializeField]
        private AttackCombo[] _attackCombos;

        [FoldoutGroup("References"), SerializeField]
        private RaySensor2D _sensor;

        private int _currentComboIndex;
        private Animator _animator;

        public UnityAction onComboFinished;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack()
        {
            _attackCombos[CurrentComboIndex - 1].SetRayAttackDistance(_sensor);

            foreach (Damageable damageable in _sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage();
            }
        }

        public void UseCombo()
        {
            CurrentComboIndex++;

            _animator.SetInteger("combo", Array.IndexOf(_attackCombos, _attackCombos[CurrentComboIndex - 1]) + 1);
        }

        public void ResetCombo()
        {
            CurrentComboIndex = 0;
            _sensor.Length = 0;
            _animator.SetInteger("combo", 0);

            onComboFinished?.Invoke();
        }
    }
}