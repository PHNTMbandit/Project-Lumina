using System;
using Sirenix.OdinInspector;
using UnityEngine;

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
public class StatusEffectTier
{
    [EnumPaging]
    public TierLevel tierLevel;

    [SerializeReference]
    public IStatusEffect[] tierStatusEffects;
}