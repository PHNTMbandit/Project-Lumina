using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegrationIntegration
{
    [TaskCategory("MasterAudio/Ducking")]
    [TaskDescription("Add a Sound Group to the list of sounds that cause music ducking.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
    public class AddDuckingGroup : Action
    {
        [Tooltip("Name of Master Audio Sound Group")]
        public SharedString soundGroupName;
        [Tooltip("Percentage of sound played to start unducking")]
        public SharedFloat riseVolumeStart;
        [Tooltip("Amount of decimals to cut the original volume")]
        public SharedFloat duckedVolCut;
        [Tooltip("Amount of time to return music to original volume")]
        public SharedFloat unduckTime;

        public override TaskStatus OnUpdate()
        {
            MasterAudio.AddSoundGroupToDuckList(soundGroupName.Value, riseVolumeStart.Value, duckedVolCut.Value, unduckTime.Value);

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            soundGroupName = "";
            var defaultRise = .5f;
            var ma = MasterAudio.Instance;
            if (ma != null) {
                defaultRise = ma.defaultRiseVolStart;
            }
            riseVolumeStart = defaultRise;
            duckedVolCut = 0f;
            unduckTime = 0f;
        }
    }
}