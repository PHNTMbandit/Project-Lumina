using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
    [TaskCategory("MasterAudio/Playlist")]
    [TaskDescription("Fade the Playlist volume to a specific volume over X seconds.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class FadePlaylist : Action
    {
        [Tooltip("Check this to perform action on all Playlist Controllers")]
        public SharedBool allPlaylistControllers;
        [Tooltip("Name of Playlist Controller to use. Not required if you only have one")]
        public SharedString playlistControllerName;
        [Tooltip("Target Playlist Volume")]
        public SharedFloat targetVolume;
        [Tooltip("Amount of time to complete fade (seconds)")]
        public SharedFloat fadeTime;

        public override TaskStatus OnUpdate()
        {
            if (allPlaylistControllers.Value) {
                var pcs = PlaylistController.Instances;

                for (var i = 0; i < pcs.Count; i++) {
                    MasterAudio.FadePlaylistToVolume(pcs[i].name, targetVolume.Value, fadeTime.Value);
                }
            } else {
                if (string.IsNullOrEmpty(playlistControllerName.Value)) {
                    MasterAudio.FadePlaylistToVolume(targetVolume.Value, fadeTime.Value);
                } else {
                    MasterAudio.FadePlaylistToVolume(playlistControllerName.Value, targetVolume.Value, fadeTime.Value);
                }
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            allPlaylistControllers = false;
            playlistControllerName = "";
            targetVolume = 0;
            fadeTime = 0;
        }
    }
}