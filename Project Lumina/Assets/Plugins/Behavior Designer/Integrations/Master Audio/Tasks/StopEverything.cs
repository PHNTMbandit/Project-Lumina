using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Everything")]
    [TaskDescription("Stop all sound effects and Playlists.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class StopEverything : Action
    {
        public override TaskStatus OnUpdate()
        {
            MasterAudio.StopEverything();

            return TaskStatus.Success;
        }
    }
}