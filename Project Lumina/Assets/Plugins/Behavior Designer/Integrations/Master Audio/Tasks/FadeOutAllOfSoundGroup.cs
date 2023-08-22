using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Group")]
    [TaskDescription("Fade all of a Sound Group to zero volume over X seconds.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class FadeOutAllOfSoundGroup : Action
    {
        [Tooltip("Check this to perform action on all Sound Groups")]
        public SharedBool allGroups;
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;
        [Tooltip("Amount of time to complete fade (seconds)")]
        public SharedFloat fadeTime;

        public override TaskStatus OnUpdate()
        {
            if (!allGroups.Value && string.IsNullOrEmpty(soundGroupName.Value)) {
                Debug.LogError("You must either check 'All Groups' or enter the Sound Group Name");
                return TaskStatus.Failure;
            }

            if (allGroups.Value) {
                var groupNames = MasterAudio.RuntimeSoundGroupNames;
                for (var i = 0; i < groupNames.Count; i++) {
                    MasterAudio.FadeOutAllOfSound(groupNames[i], fadeTime.Value);
                }
            } else {
                MasterAudio.FadeOutAllOfSound(soundGroupName.Value, fadeTime.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            if (allGroups != null) {
                allGroups = false;
            }
            if (soundGroupName != null) {
                soundGroupName = "";
            }
            if (fadeTime != null) {
                fadeTime.Value = 0;
            }
        }
    }
}