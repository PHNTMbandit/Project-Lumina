using BehaviorDesigner.Runtime;
using Micosmo.SensorToolkit;
using ProjectLumina.Data;

namespace ProjectLumina.AI.SharedTypes
{
    [System.Serializable]
    public class SharedStat : SharedVariable<Stat>
    {
        public static implicit operator SharedStat(Stat value) => new() { Value = value };
    }
}