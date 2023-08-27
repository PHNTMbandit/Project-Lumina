using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs
{
    [Serializable]
    public enum NeuroglyphType
    {
        Blessing,
        Hex,
    }

    [CreateAssetMenu(fileName = "New Neuroglyph", menuName = "Project Lumina/Neuroglyph", order = 3)]
    public class Neuroglyph : ScriptableObject
    {
        [field: SerializeField, PreviewField(Alignment = ObjectFieldAlignment.Left)]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public string NeuroglyphName { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: SerializeField, EnumToggleButtons]
        public NeuroglyphType NeuroglyphType { get; private set; }

        [SerializeField, EnumToggleButtons]
        private TierLevel _currentTierLevel;

        [SerializeField, HideLabel, TableList(AlwaysExpanded = true)]
        private NeuroglyphTier[] _neuroglyphTierEffects = new NeuroglyphTier[9];

        public virtual void Apply(GameObject target)
        {
            foreach (INeuroglyphStrategy statusEffect in GetCurrentStatusEffectTier().tierEffects)
            {
                statusEffect.Activate(target);
            }
        }

        public virtual void Revert(GameObject target)
        {
            foreach (INeuroglyphStrategy statusEffect in GetCurrentStatusEffectTier().tierEffects)
            {
                statusEffect.Deactivate(target);
            }
        }

        public NeuroglyphTier GetStatusEffectTierByLevel(TierLevel level)
        {
            return Array.Find(_neuroglyphTierEffects, i => i.tierLevel == level);
        }

        public NeuroglyphTier GetCurrentStatusEffectTier()
        {
            return Array.Find(_neuroglyphTierEffects, i => i.tierLevel == _currentTierLevel);
        }
    }
}