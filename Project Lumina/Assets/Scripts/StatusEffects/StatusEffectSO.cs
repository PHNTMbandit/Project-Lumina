using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.StatusEffects
{
    [Serializable]
    public enum StatusEffectType
    {
        Buff,
        Debuff,
    }

    [CreateAssetMenu(fileName = "New Status Effect", menuName = "Project Lumina/Status Effect", order = 3)]
    public class StatusEffectSO : ScriptableObject
    {
        [field: SerializeField, PreviewField(Alignment = ObjectFieldAlignment.Left)]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public string StatusEffectName { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: SerializeField, EnumToggleButtons]
        public StatusEffectType StatusEffectType { get; private set; }

        [SerializeField, EnumToggleButtons]
        private TierLevel _currentTierLevel;

        [SerializeField, HideLabel, TableList(AlwaysExpanded = true)]
        private StatusEffectTier[] _tierStatusEffects = new StatusEffectTier[9];

        public virtual void Apply(GameObject target)
        {
            foreach (IStatusEffect statusEffect in GetCurrentStatusEffectTier().tierStatusEffects)
            {
                statusEffect.Activate(target);
            }
        }

        public virtual void Revert(GameObject target)
        {
            foreach (IStatusEffect statusEffect in GetCurrentStatusEffectTier().tierStatusEffects)
            {
                statusEffect.Deactivate(target);
            }
        }

        public StatusEffectTier GetStatusEffectTierByLevel(TierLevel level)
        {
            return Array.Find(_tierStatusEffects, i => i.tierLevel == level);
        }

        public StatusEffectTier GetCurrentStatusEffectTier()
        {
            return Array.Find(_tierStatusEffects, i => i.tierLevel == _currentTierLevel);
        }
    }
}