using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio")]
    [TaskDescription("Play a Sound.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class PlaySound : Action
    {
        [Tooltip("The GameObject to use for sound position.")]
        public SharedGameObject targetGameObject;
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;
        [Tooltip("Name of specific variation (optional)")]
        public SharedString variationName;
        [Tooltip("Volume of the sound")]
        public SharedFloat volume;
        [Tooltip("Seconds to delay the sound from playing")]
        public SharedFloat delaySound;
        [Tooltip("Play the sound as a 3D sound?")]
        public SharedBool useThisLocation;
        [Tooltip("Attach the sound to the current GameObject?")]
        public SharedBool attachToGameObject;
        [Tooltip("Use a fixed pitch?")]
        public SharedBool useFixedPitch;
        [Tooltip("Fixed Pitch will be used only if 'Use Fixed Pitch' is checked above.")]
        public SharedFloat fixedPitch;

        public override TaskStatus OnUpdate()
        {
            var groupName = soundGroupName.Value;
            var childName = variationName.Value;
            var willAttach = attachToGameObject.Value;
            var use3dLocation = useThisLocation.Value;
            var vol = volume.Value;
            var fDelay = delaySound.Value;
            float? pitch = fixedPitch.Value;
            if (!useFixedPitch.Value) {
                pitch = null;
            }

            if (string.IsNullOrEmpty(childName)) {
                childName = null;
            }

            Transform trans = GetDefaultGameObject(targetGameObject.Value).transform;
            if (!use3dLocation && !willAttach) {
                MasterAudio.PlaySoundAndForget(groupName, vol, pitch, fDelay, childName);
            } else if (!willAttach) {
                MasterAudio.PlaySound3DAtVector3AndForget(groupName, trans.position, vol, pitch, fDelay, childName);
            } else {
                MasterAudio.PlaySound3DFollowTransform(groupName, trans, vol, pitch, fDelay, childName);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            soundGroupName = "";
            volume = 1;
            delaySound = 0;
            useThisLocation = true;
            attachToGameObject = false;
            useFixedPitch = false;
            fixedPitch = 1;
        }
    }
}