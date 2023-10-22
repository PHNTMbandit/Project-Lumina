using System;
using ProjectLumina.Interfaces;
using ProjectLumina.Neuroglyphs.Components;
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

    [CreateAssetMenu(fileName = "New Neuroglyph", menuName = "Project Lumina/Neuroglyphs/Neuroglyph", order = 3)]
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

        [SerializeField, TableList(AlwaysExpanded = true)]
        private NeuroglyphTier[] _neuroglyphTierEffects = new NeuroglyphTier[9];

        public virtual void Apply(GameObject user)
        {
            foreach (NeuroglyphComponent statusEffect in GetCurrentTier().tierEffects)
            {
                statusEffect.Activate(user);
            }
        }

        public virtual void Revert(GameObject user)
        {
            foreach (NeuroglyphComponent statusEffect in GetCurrentTier().tierEffects)
            {
                statusEffect.Deactivate(user);
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