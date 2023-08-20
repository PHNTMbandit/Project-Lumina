using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Controllers;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Abilities
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Melee Attack")]
    public class CharacterMeleeAttack : CharacterAbility
    {
        public int CurrentMeleeAttackIndex
        {
            get => _currentMeleeAttackIndex;
            set => _currentMeleeAttackIndex = value <= 0 ? 0 : value >= 4 ? 4 : value;
        }

        [ToggleGroup("Slash1"), SerializeField]
        private bool Slash1;

        [ToggleGroup("Slash1"), SerializeField]
        private Attack Slash1Attack;

        [ToggleGroup("Slash2"), SerializeField]
        private bool Slash2;

        [ToggleGroup("Slash2"), SerializeField]
        private Attack Slash2Attack;

        [ToggleGroup("Slam"), SerializeField]
        private bool Slam;

        [ToggleGroup("Slam"), SerializeField]
        private Attack SlamAttack;

        [ToggleGroup("Spin"), SerializeField]
        private bool Spin;

        [ToggleGroup("Spin"), SerializeField]
        private Attack SpinAttack;

        private int _currentMeleeAttackIndex = 1;
        private Animator _animator;

        public UnityAction onComboFinished;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void UseMeleeAttack()
        {
            switch (CurrentMeleeAttackIndex)
            {
                case 1:
                    if (Slash1)
                    {
                        _animator.SetInteger("melee attack", CurrentMeleeAttackIndex);
                        CurrentMeleeAttackIndex++;
                    }
                    break;

                case 2:
                    if (Slash2)
                    {
                        _animator.SetInteger("melee attack", CurrentMeleeAttackIndex);
                        CurrentMeleeAttackIndex++;
                    }
                    break;

                case 3:
                    if (Slam)
                    {
                        _animator.SetInteger("melee attack", CurrentMeleeAttackIndex);
                        CurrentMeleeAttackIndex++;
                    }
                    break;

                case 4:
                    if (Spin)
                    {
                        _animator.SetInteger("melee attack", CurrentMeleeAttackIndex);
                        CurrentMeleeAttackIndex++;
                    }
                    break;
            }
        }

        public void MeleeAttack()
        {
            switch (CurrentMeleeAttackIndex)
            {
                case 1:
                    Damage(Slash1Attack);
                    break;

                case 2:
                    Damage(Slash2Attack);
                    break;

                case 3:
                    Damage(SlamAttack);
                    break;

                case 4:
                    Damage(SpinAttack);
                    break;
            }
        }

        public void ResetCombo()
        {
            CurrentMeleeAttackIndex = 1;

            onComboFinished?.Invoke();
        }

        private void Damage(Attack attack)
        {
            foreach (Damageable damageable in attack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(attack.Damage);

                ObjectPoolController.Instance.GetPooledObject(attack.HitFX.name, damageable.transform.position, false);
            }
        }
    }
}