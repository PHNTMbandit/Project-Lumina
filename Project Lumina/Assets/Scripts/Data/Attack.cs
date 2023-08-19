using System;
using Micosmo.SensorToolkit;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectLumina.Data
{
    [Serializable, HideLabel]
    public class Attack
    {
        [field: Range(0, 100), SerializeField]
        public float Damage { get; private set; }

        [field: SerializeField]
        public RangeSensor2D Sensor { get; private set; }
    }
}
