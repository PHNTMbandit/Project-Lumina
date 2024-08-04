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
        public int CurrentMeleeAttack
        {
            get => _currentMeleeComboIndex;
            set =>
                _currentMeleeComboIndex =
                    value <= 0
                        ? 0
                        : value >= _attacks.Length
                            ? _attacks.Length
                            : value;
        }

        [BoxGroup("Attacks"), SerializeField]
        private MeleeAttack[] _attacks;

        private int _currentMeleeComboIndex;
        private MeleeAttack _currentMeleeAttack;
        public UnityAction<GameObject> onHit;

        public bool CanNextCombo(int stateLength)
        {
            if (CurrentMeleeAttack < stateLength)
            {
                _currentMeleeAttack = _attacks[CurrentMeleeAttack];

                if (_currentMeleeAttack.IsUnlocked)
                {
                    CurrentMeleeAttack++;
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
            CurrentMeleeAttack = 0;
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

        public MeleeAttack[] GetMeleeAttacks()
        {
            return _attacks;
        }
    }
}
