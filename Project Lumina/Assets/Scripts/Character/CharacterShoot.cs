using System.Collections.Generic;
using ProjectLumina.Capabilities;
using ProjectLumina.Data;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectLumina.Character
{
    [AddComponentMenu("Character/Character Shoot")]
    public class CharacterShoot : CharacterAbility
    {
        [field: BoxGroup("Attacks"), SerializeField]
        public RangedAttack Attack { get; private set; }

        [BoxGroup("Settings"), SerializeField, Range(0, 10)]
        private float _fireRate;

        private float _nextFire;

        public UnityAction<GameObject> onHit;

        public bool CanShoot()
        {
            if (Time.time >= _nextFire)
            {
                if (Attack.IsUnlocked)
                {
                    _nextFire = Time.time + _fireRate;

                    return true;
                }

                return false;
            }

            return false;
        }

        public void DealRangedDamage()
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
