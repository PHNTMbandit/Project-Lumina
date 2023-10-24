using ProjectLumina.Data.StatusEffects;
using UnityEngine;

namespace ProjectLumina.Factories
{
    public class StatusEffectFactory
    {
        public StatusEffect GetStatusEffect(StatusEffect statusEffect)
        {
            return Object.Instantiate(statusEffect);
        }
    }
}