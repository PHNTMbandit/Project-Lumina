using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Playlist")]
    [TaskDescription("Get the name of the currently playing Audio Clip in a Playlist.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class GetCurrentPlaylistClipName : Action
    {
        [Tooltip("Name of Playlist Controller. Not required if you only have one Playlist Controller")]
        public SharedString playlistControllerName;
        [Tooltip("Name of Variable to store the current clip name in")]
        [RequiredField]
        public SharedString storeResult;

        public override TaskStatus OnUpdate()
        {
            PlaylistController controller = null;

            if (!string.IsNullOrEmpty(playlistControllerName.Value)) {
                controller = PlaylistController.InstanceByName(playlistControllerName.Value);
            } else {
                controller = MasterAudio.OnlyPlaylistController;
            }

            var clip = controller.CurrentPlaylistClip;

            storeResult.Value = clip == null ? string.Empty : clip.name;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            playlistControllerName = "";
            storeResult = "";
        }
    }
}