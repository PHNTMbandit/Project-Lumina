using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Neuroglyphs
{
    [Serializable]
    public enum TierLevel
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
        public TierLevel tierLevel;

        [SerializeReference]
        public INeuroglyphStrategy[] tierEffects;
    }
}