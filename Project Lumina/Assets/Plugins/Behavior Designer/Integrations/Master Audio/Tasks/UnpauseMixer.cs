using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Mixer")]
    [TaskDescription("Unpause all sound effects. Does not include Playlists.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class UnpauseMixer : Action
    {
        public override TaskStatus OnUpdate()
        {
            MasterAudio.UnpauseMixer();

            return TaskStatus.Success;
        }
    }
}