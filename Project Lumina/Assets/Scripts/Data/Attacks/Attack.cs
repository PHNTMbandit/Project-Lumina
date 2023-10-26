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
        [field: BoxGroup("Stats"), ToggleLeft, SerializeField]
        public bool IsUnlocked { get; private set; }

        [BoxGroup("Stats"), Range(0, 100), SerializeField]
        protected float damage;

        [BoxGroup("Stats"), Range(0, 100), SuffixLabel("%"), SerializeField]
        protected float criticalChance, criticalDamageMultiplier;

        [BoxGroup("Effects"), Range(0, 0.1f), SerializeField]
        protected float hitStopDuration;

        [BoxGroup("Effects"), SerializeField]
        protected GameObject hitFX;

        [BoxGroup("Effects"), ColorPalette, SerializeField]
        protected Color damageIndicatorColour, criticalDamageIndicatorColour;

        [field: BoxGroup("References"), SerializeField]
        public RangeSensor2D Sensor { get; private set; }

        public abstract bool TryAttack(GameObject user, Damageable target);
    }
}
