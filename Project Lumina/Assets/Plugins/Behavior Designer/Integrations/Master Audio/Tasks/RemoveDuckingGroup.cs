using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Ducking")]
    [TaskDescription("Remove a Sound Group from the list of sounds that cause music ducking.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class RemoveDuckingGroup : Action
    {
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;

        public override TaskStatus OnUpdate()
        {
            MasterAudio.RemoveSoundGroupFromDuckList(soundGroupName.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            soundGroupName = "";
        }
    }
}