using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Mixer")]
    [TaskDescription("Stop all sound effects. Does not include Playlists.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class StopMixer : Action
    {
        public override TaskStatus OnUpdate()
        {
            MasterAudio.StopMixer();

            return TaskStatus.Success;
        }
    }
}