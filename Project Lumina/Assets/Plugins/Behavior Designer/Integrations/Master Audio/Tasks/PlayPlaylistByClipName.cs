using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Playlist")]
    [TaskDescription("Play a clip in the current Playlist by name.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class PlayPlaylistByClipName : Action
    {
        [Tooltip("Name of Playlist Controller to use. Not required if you only have one")]
        public SharedString playlistControllerName;
        [Tooltip("Name of playlist clip to play")]
        public SharedString clipName;

        public override TaskStatus OnUpdate()
        {
            if (string.IsNullOrEmpty(playlistControllerName.Value)) {
                MasterAudio.TriggerPlaylistClip(clipName.Value);
            } else {
                MasterAudio.TriggerPlaylistClip(playlistControllerName.Value, clipName.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            clipName = "";
            playlistControllerName = "";
        }
    }
}