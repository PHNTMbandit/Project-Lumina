using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu("Character/Character Melee Attack")]
    public class CharacterMeleeAttack : CharacterAbility
    {
        [BoxGroup("Attack Combos"), SerializeField]
        private MeleeAttack[] _attacks;

        private Attack _currentMeleeAttack;
        private Animator _animator;

        public UnityAction<GameObject> onHit;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update() { }

        public void MeleeAttack() { }

        public void TryMeleeAttack()
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
