using ProjectLumina.Effects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data.StatusEffects
{
    [CreateAssetMenu(fileName = "New Poison Status Effect", menuName = "Project Lumina/Status Effects/Poison", order = 0)]
    public class PoisonStatusEffect : StatusEffect
    {
        [TabGroup("Stats"), Range(0, 10), SerializeField]
        private float _tickInterval = 1.0f;

        private float _timeSinceLastTick;

        public override void AddStatusEffect(GameObject target)
        {
            base.AddStatusEffect(target);

            _timeSinceLastTick = 0;
        }

        public override void UpdateStatusEffect()
        {
            base.UpdateStatusEffect();

            if (_timeSinceLastTick >= _tickInterval)
            {
                _timeSinceLastTick = 0;

                DealPoisonDamage();
            }
        }

        private void DealPoisonDamage()
        {
            target.Damage(damage.Value);

            if (target.TryGetComponent(out DamageIndicator damageIndicator))
            {
                damageIndicator.ShowDamageIndicator(false, damage.Value, target.transform.position, Colour);
            }
        }
    }
}