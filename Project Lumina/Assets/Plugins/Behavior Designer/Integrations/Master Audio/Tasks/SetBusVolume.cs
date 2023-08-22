using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Bus")]
    [TaskDescription("Set a single bus volume level.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class SetBusVolume : Action
    {
        [Tooltip("Check this to perform action on all Buses")]
        public SharedBool allBuses;
        [Tooltip("Name of Master Audio Bus")]
        public SharedString busName;
        [Tooltip("Volume of the bus")]
        public SharedFloat volume;
        
        public override TaskStatus OnUpdate()
        {
            if (!allBuses.Value && string.IsNullOrEmpty(busName.Value)) {
                Debug.LogError("You must either check 'All Buses' or enter the Bus Name");
                return TaskStatus.Failure;
            }

            if (allBuses.Value) {
                var busNames = MasterAudio.RuntimeBusNames;
                for (var i = 0; i < busNames.Count; i++) {
                    MasterAudio.SetBusVolumeByName(busNames[i], volume.Value);
                }
            } else {
                MasterAudio.SetBusVolumeByName(busName.Value, volume.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            allBuses = false;
            busName = "";
            volume = 1;
        }
    }
}