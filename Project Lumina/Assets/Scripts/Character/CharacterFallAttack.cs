using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [AddComponentMenu("Character/Character Fall Attack")]
    public class CharacterFallAttack : CharacterAbility
    {
        [field: BoxGroup("Attack"), SerializeField]
        public MeleeAttack Attack { get; private set; }

        public UnityAction<GameObject> onHit;

        public void DealFallAttackDamage()
        {
            foreach (
                Damageable damageable in Attack.Sensor.GetDetectedComponents(new List<Damageable>())
            )
            {
                if (Attack.TryAttack(gameObject, damageable))
                {
                    onHit?.Invoke(damageable.gameObject);
                }
            }
        }
    }
}
