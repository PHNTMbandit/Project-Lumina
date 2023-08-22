using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Group")]
    [TaskDescription("Set a single Sound Group volume level.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class SetGroupVolume : Action
    {
        [Tooltip("Check this to perform action on all Sound Groups")]
        public SharedBool allGroups;
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;
        [Tooltip("Volume of the group")]
        public SharedFloat volume;

        public override TaskStatus OnUpdate()
        {
            if (!allGroups.Value && string.IsNullOrEmpty(soundGroupName.Value)) {
                Debug.LogError("You must either check 'All Groups' or enter the Sound Group Name");
                return TaskStatus.Failure;
            }

            if (allGroups.Value) {
                var groupNames = MasterAudio.RuntimeSoundGroupNames;
                for (var i = 0; i < groupNames.Count; i++) {
                    MasterAudio.SetGroupVolume(groupNames[i], volume.Value);
                }
            } else {
                MasterAudio.SetGroupVolume(soundGroupName.Value, volume.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            allGroups = false;
            soundGroupName = "";
            volume = 1;
        }
    }
}