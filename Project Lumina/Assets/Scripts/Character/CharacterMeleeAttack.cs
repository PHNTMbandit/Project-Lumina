using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Melee Attack")]
    public class CharacterMeleeAttack : CharacterAbility
    {
        public int CurrentComboIndex
        {
            get => _currentComboIndex;
            set =>
                _currentComboIndex =
                    value <= 0
                        ? 0
                        : value >= Attacks.Length
                            ? Attacks.Length
                            : value;
        }

        [field: BoxGroup("Attacks"), SerializeField]
        public MeleeAttack[] Attacks { get; private set; }

        private int _currentComboIndex;
        private MeleeAttack _currentMeleeAttack;
        public UnityAction<GameObject> onHit;

        public bool CanNextCombo(int stateLength)
        {
            if (CurrentComboIndex < stateLength)
            {
                _currentMeleeAttack = Attacks[CurrentComboIndex];

                if (_currentMeleeAttack.IsUnlocked)
                {
                    CurrentComboIndex++;
                }
                else
                {
                    EndCombo();
                    return false;
                }

                return true;
            }

            EndCombo();
            return false;
        }

        public void EndCombo()
        {
            CurrentComboIndex = 0;
        }

        public void DealDamage()
        {
            foreach (
                Damageable damageable in _currentMeleeAttack.Sensor.GetDetectedComponents(
                    new List<Damageable>()
                )
            )
            {
                if (_currentMeleeAttack.TryAttack(gameObject, damageable))
                {
                    onHit?.Invoke(damageable.gameObject);
                }
            }
        }
    }
}
