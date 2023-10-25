using System.Text;
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
        [field: TabGroup("Details"), SerializeField, Range(0, 35)]
        public int NeuroglyphID { get; private set; }

        [field: TabGroup("Details"), SerializeField, PreviewField(Alignment = ObjectFieldAlignment.Left)]
        public Sprite Icon { get; private set; }

        [field: TabGroup("Details"), SerializeField]
        public string NeuroglyphName { get; private set; }

        [field: TabGroup("Stats"), SerializeField, EnumToggleButtons]
        public NeuroglyphType NeuroglyphType { get; private set; }

        [field: TabGroup("Stats"), SerializeField, EnumToggleButtons]
        public NeuroglyphTierLevel CurrentTierLevel { get; private set; }

        [TabGroup("Stats"), SerializeField, TableList(AlwaysExpanded = true)]
        private NeuroglyphTier[] _neuroglyphTierEffects =
        {
            new (NeuroglyphTierLevel.Tier1),
            new (NeuroglyphTierLevel.Tier2),
            new (NeuroglyphTierLevel.Tier3),
            new (NeuroglyphTierLevel.Tier4),
            new (NeuroglyphTierLevel.Tier5),
            new (NeuroglyphTierLevel.Tier6),
            new (NeuroglyphTierLevel.Tier7),
            new (NeuroglyphTierLevel.Tier8),
            new (NeuroglyphTierLevel.Tier9),
 };

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

        public string GetNeuroglyphComponentDescriptions()
        {
            StringBuilder stringBuilder = new();

            foreach (NeuroglyphComponent component in GetCurrentTier().tierEffects)
            {
                stringBuilder.AppendLine(component.GetComponentDescription());
            }

            return stringBuilder.ToString();
        }
    }
}