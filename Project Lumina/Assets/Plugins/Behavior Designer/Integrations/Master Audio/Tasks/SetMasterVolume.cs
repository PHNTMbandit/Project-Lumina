using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio")]
    [TaskDescription("Set master volume level.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class SetMasterVolume : Action
    {
        [Tooltip("The master volume level")]
        public SharedFloat volume;

        [Tooltip("Repeat every frame while the state is active.")]
        public bool everyFrame;

        public override TaskStatus OnUpdate()
        {
            MasterAudio.MasterVolumeLevel = volume.Value;
            
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            volume = 1;
        }
    }
}