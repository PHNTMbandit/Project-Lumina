using System;
using Sirenix.OdinInspector;
using UnityEngine;

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

        [SerializeReference]
        public INeuroglyphStrategy[] tierEffects;
    }
}