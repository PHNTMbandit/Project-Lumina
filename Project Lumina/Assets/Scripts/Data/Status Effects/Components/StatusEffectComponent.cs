using UnityEngine;

namespace ProjectLumina.Data.StatusEffects
{
    public abstract class StatusEffectComponent : ScriptableObject
    {
        public abstract void ApplyEffect(GameObject target);
        public abstract void UpdateEffect();
        public abstract void RemoveEffect();
        public abstract bool IsRunning();
    }
}