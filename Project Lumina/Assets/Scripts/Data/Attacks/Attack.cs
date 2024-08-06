using System;
using Micosmo.SensorToolkit;
using ProjectLumina.Capabilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data
{
    [Serializable, HideLabel]
    public abstract class Attack
    {
        [field: SerializeField, TabGroup("Stats"), ShowInInspector]
        public string AttackName { get; private set; }

        [TabGroup("Stats"), LabelText("Damage Stat"), ShowInInspector, ReadOnly]
        public Stat Damage { get; private set; } = new(0);

        [TabGroup("Stats"), LabelText("Critical Chance Stat"), ShowInInspector, ReadOnly]
        public Stat CriticalChance { get; private set; } = new(0);

        [TabGroup("Stats"), LabelText("Critical Damage Multiplier Stat"), ShowInInspector, ReadOnly]
        public Stat CriticalDamageMultiplier { get; private set; } = new(0);

        [field: TabGroup("Stats"), ToggleLeft, SerializeField]
        public bool IsUnlocked { get; private set; }

        [TabGroup("Stats"), Range(0, 100), SerializeField]
        protected float damage;

        [TabGroup("Stats"), Range(0, 100), SuffixLabel("%"), SerializeField]
        protected float criticalChance,
            criticalDamageMultiplier;

        [TabGroup("Effects"), Range(0, 0.1f), SerializeField]
        protected float hitStopDuration;

        [TabGroup("Effects"), SerializeField]
        protected GameObject hitFX;

        [TabGroup("Effects"), ColorPalette, SerializeField]
        protected Color damageIndicatorColour,
            criticalDamageIndicatorColour;

        [field: TabGroup("References"), SerializeField]
        public RangeSensor2D Sensor { get; private set; }

        public abstract bool TryAttack(GameObject user, Damageable target);
    }
}
