using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio")]
    [TaskDescription("Stop sounds made by a Transform.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class StopTransformSound : Action
    {
        [Tooltip("The Game Object to stop sounds made by")]
        public SharedGameObject targetGameObject;
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

            Transform targetTransform;
            if (targetGameObject.Value != null) {
                targetTransform = targetGameObject.Value.transform;
            } else {
                targetTransform = transform;
            }

            if (allGroups.Value) {
                MasterAudio.StopAllSoundsOfTransform(targetTransform);
            } else {
                if (string.IsNullOrEmpty(soundGroupName.Value)) {
                    Debug.LogError("You must either check 'All Groups' or enter the Sound Group Name");
                }
                MasterAudio.StopSoundGroupOfTransform(targetTransform, soundGroupName.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            allGroups = false;
            soundGroupName = "";
        }
    }
}