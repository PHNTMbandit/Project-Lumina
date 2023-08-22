using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Playlist")]
    [TaskDescription("Start a Playlist by name.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class StartPlaylistByName : Action
    {
        [Tooltip("Name of Playlist Controller to use. Not required if you only have one.")]
        public SharedString playlistControllerName;
        [Tooltip("Name of playlist to start")]
        public SharedString playlistName;

        public override TaskStatus OnUpdate()
        {
            if (string.IsNullOrEmpty(playlistControllerName.Value)) {
                MasterAudio.StartPlaylist(playlistName.Value);
            } else {
				MasterAudio.StartPlaylist(playlistControllerName.Value, playlistName.Value);
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            playlistControllerName = "";
            playlistName = "";
        }
    }
}