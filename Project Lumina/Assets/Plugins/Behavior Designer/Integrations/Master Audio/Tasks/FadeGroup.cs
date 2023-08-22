using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Group")]
    [TaskDescription("Fade a Sound Group to a specific volume over X seconds.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class FadeGroup : Action
    {
        [Tooltip("Check this to perform action on all Sound Groups")]
        public SharedBool allGroups;
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;
        [Tooltip("Target Sound Group volume")]
        public SharedFloat targetVolume;
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
                    MasterAudio.FadeSoundGroupToVolume(groupNames[i], targetVolume.Value, fadeTime.Value);
                }
            } else {
                MasterAudio.FadeSoundGroupToVolume(soundGroupName.Value, targetVolume.Value, fadeTime.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            allGroups = false;
            soundGroupName = "";
            targetVolume = 0;
            fadeTime = 0;
        }
    }
}