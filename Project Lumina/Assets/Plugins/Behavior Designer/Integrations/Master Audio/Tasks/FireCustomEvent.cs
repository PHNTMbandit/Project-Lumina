using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DarkTonic.MasterAudio;

namespace BehaviorDesigner.Runtime.Tasks.MasterAudioIntegration
{
	[TaskCategory("MasterAudio")]
	[TaskDescription("Fire a Custom Event.")]
	[TaskIcon("Assets/Behavior Designer/Integrations/Master Audio/Editor/MasterAudioIcon.png")]
	public class FireCustomEvent : Action
	{
		[Tooltip("The Custom Event (defined in Master Audio prefab) to fire")]
		public SharedString customEventName;
		
		[Tooltip("The Custom Event's origin point.")]
		public SharedTransform eventOriginPoint;
		
		public override TaskStatus OnUpdate()
		{
			MasterAudio.FireCustomEvent(customEventName.Value, eventOriginPoint.Value);
			
			return TaskStatus.Success;
		}
		
		public override void OnReset()
        {
            customEventName = "";
            eventOriginPoint = null;
		}
	}
}	