using BehaviorDesigner.Runtime.Tasks;
using ProjectLumina.AI.SharedTypes;
using UnityEngine;

namespace ProjectLumina.AI.Conditionals
{
    [TaskCategory("Sensor")]
    [TaskIcon("c3873913d6f08e44d8f24b80257edf45", "7f2d1486b1b44ec4b8c213df246534c5")]
    public class IsTargetInSight : Conditional
    {
        [SerializeField]
        private SharedSensor _sensor;

        public override TaskStatus OnUpdate()
        {
            if (_sensor.Value.GetNearestDetection() != null)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}