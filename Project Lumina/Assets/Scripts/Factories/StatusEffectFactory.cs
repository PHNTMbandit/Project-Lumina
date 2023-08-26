using ProjectLumina.StatusEffects;
using UnityEngine;

namespace ProjectLumina.Factories
{
    public class StatusEffectFactory
    {
        public StatusEffectSO GetStatusEffect(StatusEffectSO statusEffect)
        {
            return ScriptableObject.CreateInstance<StatusEffectSO>();
        }
    }
}