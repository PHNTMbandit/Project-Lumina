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
        [field: SerializeField, Range(0, 35)]
        public int NeuroglyphID { get; private set; }

        [field: SerializeField, PreviewField(Alignment = ObjectFieldAlignment.Left)]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public string NeuroglyphName { get; private set; }

        [field: SerializeField, TextArea]
        public string Description { get; private set; }

        [field: SerializeField, EnumToggleButtons]
        public NeuroglyphType NeuroglyphType { get; private set; }

        [field: SerializeField, EnumToggleButtons]
        public NeuroglyphTierLevel CurrentTierLevel { get; private set; }

        [SerializeField, HideLabel, TableList(AlwaysExpanded = true)]
        private NeuroglyphTier[] _neuroglyphTierEffects = new NeuroglyphTier[9];

        public virtual void Apply(GameObject target)
        {
            foreach (INeuroglyphStrategy statusEffect in GetCurrentTier().tierEffects)
            {
                statusEffect.Activate(target);
            }
        }

        public virtual void Revert(GameObject target)
        {
            foreach (INeuroglyphStrategy statusEffect in GetCurrentTier().tierEffects)
            {
                statusEffect.Deactivate(target);
            }
        }

        public NeuroglyphTier GetCurrentTier()
        {
            return Array.Find(_neuroglyphTierEffects, i => i.tierLevel == CurrentTierLevel);
        }

        public void Upgrade(int levelUpAmount)
        {
            CurrentTierLevel = (NeuroglyphTierLevel)levelUpAmount;
        }
    }
}