using System;
using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data
{
    [Serializable, HideLabel]
    public class Attack
    {
        [field: ToggleLeft, SerializeField]
        public bool IsUnlocked { get; private set; }

        [field: Range(0, 100), SerializeField]
        public float Damage { get; private set; }

        [field: Range(0, 0.1f), SerializeField]
        public float HitStopDuration { get; private set; }

        [field: SerializeField]
        public AnimationClip AttackAnimation;

        [field: SerializeField]
        public GameObject HitFX;

        [field: ColorPalette, SerializeField]
        public Color Colour;

        [field: SerializeField]
        public RangeSensor2D Sensor { get; private set; }
    }
}
