using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Group")]
    [TaskDescription("Unsolo a Sound Group.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class UnsoloGroup : Action
    {
        [Tooltip("Check this to perform action on all Sound Groups")]
        public SharedBool allGroups;
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;

        public override TaskStatus OnUpdate()
        {
            if (!allGroups.Value && string.IsNullOrEmpty(soundGroupName.Value)) {
                Debug.LogError("You must either check 'All Groups' or enter the Sound Group Name");
                return TaskStatus.Failure;
            }

            if (allGroups.Value) {
                var groupNames = MasterAudio.RuntimeSoundGroupNames;
                for (var i = 0; i < groupNames.Count; i++) {
                    MasterAudio.UnsoloGroup(groupNames[i]);
                }
            } else {
                MasterAudio.UnsoloGroup(soundGroupName.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            allGroups = false;
            soundGroupName = "";
        }
    }
}