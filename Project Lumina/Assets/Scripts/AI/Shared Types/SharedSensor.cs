using BehaviorDesigner.Runtime;
using Micosmo.SensorToolkit;

namespace ProjectLumina.AI.SharedTypes
{
    [System.Serializable]
    public class SharedSensor : SharedVariable<Sensor>
    {
        public static implicit operator SharedSensor(Sensor value) => new() { Value = value };
    }
}