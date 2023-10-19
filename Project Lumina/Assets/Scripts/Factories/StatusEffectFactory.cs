using ProjectLumina.Data.StatusEffects;
using UnityEngine;

namespace ProjectLumina.Factories
{
    public class StatusEffectFactory
    {
        public StatusEffect GetNeuroglyph(StatusEffect statusEffect)
        {
            return Object.Instantiate(statusEffect);
        }
    }
}