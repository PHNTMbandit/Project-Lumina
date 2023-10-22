using UnityEngine;

namespace ProjectLumina.Data.StatusEffects
{
    [CreateAssetMenu(fileName = "New Poison Status Effect", menuName = "Project Lumina/Status Effects/Poison", order = 0)]
    public class PoisonStatusEffect : StatusEffect
    {
        protected override void ActivateStatusEffect()
        {
            target.Damage(damage.Value);
        }
    }
}