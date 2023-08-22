using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Ducking")]
    [TaskDescription("Turn music ducking on or off.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class ToggleDucking : Action
    {
        [Tooltip("Check this to enable ducking, uncheck it to disable ducking")]
        public SharedBool enableDucking;

        public override TaskStatus OnUpdate()
        {
            MasterAudio.Instance.EnableMusicDucking = enableDucking.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            enableDucking = false;
        }
    }
}