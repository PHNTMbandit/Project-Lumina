using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Bus")]
    [TaskDescription("Fade a Bus to a specific volume over X seconds.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class FadeBus : Action
    {
        [Tooltip("Check this to perform action on all Buses")]
        public SharedBool allBuses;
        [Tooltip("Name of Master Audio Bus")]
        public SharedString busName;
        [Tooltip("Target Bus volume")]
        public SharedFloat targetVolume;
        [Tooltip("Amount of time to complete fade (seconds)")]
        public SharedFloat fadeTime;

        public override TaskStatus OnUpdate()
        {
            if (!allBuses.Value && string.IsNullOrEmpty(busName.Value)) {
                Debug.LogError("You must either check 'All Buses' or enter the Bus Name");
                return TaskStatus.Failure;
            }

            if (allBuses.Value) {
                var busNames = MasterAudio.RuntimeBusNames;
                for (var i = 0; i < busNames.Count; i++) {
                    MasterAudio.FadeBusToVolume(busNames[i], targetVolume.Value, fadeTime.Value);
                }
            } else {
                MasterAudio.FadeBusToVolume(busName.Value, targetVolume.Value, fadeTime.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            allBuses = false;
            busName = "";
            targetVolume = 0;
            fadeTime = 0;
        }
    }
}