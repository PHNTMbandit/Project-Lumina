using ProjectLumina.Effects;
using UnityEngine;

namespace ProjectLumina.Data.StatusEffects
{
    [CreateAssetMenu(fileName = "New Poison Status Effect", menuName = "Project Lumina/Status Effects/Poison", order = 0)]
    public class PoisonStatusEffect : StatusEffect
    {
        protected override void ActivateStatusEffect()
        {
            target.Damage(damage.Value);

            if (target.TryGetComponent(out DamageIndicator damageIndicator))
            {
                damageIndicator.ShowDamageIndicator(damage.Value, target.transform.position, Colour);
            }
        }
    }
}