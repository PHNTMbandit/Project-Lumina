using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Everything")]
    [TaskDescription("Pause all sound effects and Playlists.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class PauseEverything : Action
    {
        public override TaskStatus OnUpdate()
        {
            MasterAudio.PauseEverything();

            return TaskStatus.Success;
        }
    }
}