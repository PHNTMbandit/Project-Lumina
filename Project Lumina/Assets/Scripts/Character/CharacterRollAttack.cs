using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Roll Attack")]
    public class CharacterRollAttack : CharacterAbility
    {
        [field: BoxGroup("Attack"), SerializeField]
        public MeleeAttack Attack { get; private set; }

        [BoxGroup("Settings"), Range(0, 10), SerializeField]
        private float _rollAttackSpeedDamping;

        private Rigidbody2D _rb;

        public UnityAction<GameObject> onHit;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void RollAttack()
        {
            _rb.velocity /= _rollAttackSpeedDamping;
        }

        public void DealRollAttackDamage()
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
