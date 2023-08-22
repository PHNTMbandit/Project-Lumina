using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Variation")]
    [TaskDescription("Change the pich of a variation (or all variations) in a Sound Group.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class ChangeVariationPitch : Action
    {
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;
        [Tooltip("Name of specific variation (optional)")]
        public SharedString variationName;
        [Tooltip("Should all variations change?")]
        public SharedBool changeAllVariations;
        [Tooltip("The value of the pitch")]
        public SharedFloat pitch;

        public override TaskStatus OnUpdate()
        {
            var groupName = soundGroupName.Value;
            var childName = variationName.Value;

            if (string.IsNullOrEmpty(childName)) {
                childName = null;
            }

            MasterAudio.ChangeVariationPitch(groupName, changeAllVariations.Value, childName, pitch.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            soundGroupName = "";
            variationName = "";
            changeAllVariations = false;
            pitch = 1f;
        }
    }
}