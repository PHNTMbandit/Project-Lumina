using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Playlist")]
    [TaskDescription("Set the Playlist Master volume to a specific volume.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class SetPlaylistVolume : Action
    {
        [Tooltip("Playlist New Volume")]
        public SharedFloat newVolume;

        public override TaskStatus OnUpdate()
        {
            MasterAudio.PlaylistMasterVolume = newVolume.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            newVolume = 0;
        }
    }
}