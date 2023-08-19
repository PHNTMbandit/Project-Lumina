using System.Collections.Generic;
using Micosmo.SensorToolkit;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Abilities
{
    [AddComponentMenu("Character/Character Roll Attack")]
    public class CharacterRollAttack : CharacterAbility
    {
        public bool IsRollAttacking { get; private set; }

        [BoxGroup("Attack"), SerializeField]
        private Attack _rollAttack;

        public void UseRollAttack()
        {
            IsRollAttacking = true;
        }

        public void RollAttack()
        {
            foreach (Damageable damageable in _rollAttack.Sensor.GetDetectedComponents(new List<Damageable>()))
            {
                damageable.Damage(_rollAttack.Damage);
            }
        }

        public void FinishRollAttack()
        {
            IsRollAttacking = false;
        }
    }
}