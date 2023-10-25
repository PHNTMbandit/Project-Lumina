using System;
using ProjectLumina.Neuroglyphs.Components;
using Sirenix.OdinInspector;

namespace ProjectLumina.Neuroglyphs
{
    [Serializable]
    public enum NeuroglyphTierLevel
    {
        Tier1,
        Tier2,
        Tier3,
        Tier4,
        Tier5,
        Tier6,
        Tier7,
        Tier8,
        Tier9,
    }

    [Serializable]
    public class NeuroglyphTier
    {
        [EnumPaging]
        public NeuroglyphTierLevel tierLevel;

        public NeuroglyphComponent[] tierEffects;

        public NeuroglyphTier(NeuroglyphTierLevel tierLevel)
        {
            this.tierLevel = tierLevel;
        }
    }
}