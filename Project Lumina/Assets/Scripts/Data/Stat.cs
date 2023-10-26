using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ProjectLumina.Data
{
    [HideLabel, Serializable]
    public class Stat
    {
        public Stat(float baseValue)
        {
            _baseValue = baseValue;
            _modifiers = new();
        }

        private float _baseValue;
        private readonly List<StatModifier> _modifiers;

        [ShowInInspector]
        public float Value
        {
            get
            {
                float modifiedValue = _baseValue;
                foreach (var modifier in _modifiers)
                {
                    modifiedValue = modifier.Apply(modifiedValue);
                }
                return modifiedValue;
            }
        }

        public void AddModifier(StatModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(StatModifier modifier)
        {
            _modifiers.Remove(modifier);
        }

        public void ClearModifiers()
        {
            _modifiers.Clear();
        }

        public void SetBaseValue(float value)
        {
            _baseValue = value;
        }
    }

    public abstract class StatModifier
    {
        public abstract float Apply(float value);
    }

    public class FlatStatModifier : StatModifier
    {
        private readonly float _value;

        public FlatStatModifier(float value)
        {
            _value = value;
        }

        public override float Apply(float value)
        {
            return value + _value;
        }
    }

    public class PercentageStatModifier : StatModifier
    {
        private readonly float _percentage;

        public PercentageStatModifier(float percentage)
        {
            _percentage = percentage;
        }

        public override float Apply(float value)
        {
            return value * (1 + _percentage / 100f);
        }
    }
}
