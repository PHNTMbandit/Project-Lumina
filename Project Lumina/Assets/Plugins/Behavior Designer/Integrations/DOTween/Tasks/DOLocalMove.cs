using UnityEngine;
using DG.Tweening;

namespace BehaviorDesigner.Runtime.Tasks.DOTween
{
    [TaskCategory("DOTween")]
    [TaskDescription("Tween the GameObject's local position.")]
    [TaskIcon("Assets/Behavior Designer/Integrations/DOTween/Editor/Icon.png")]
    public class DOLocalMove : Action
    {
        [Tooltip("The GameObject to tween")]
        public SharedGameObject targetGameObject;
        [Tooltip("The target tween value")]
        public SharedVector3 to;
        [Tooltip("The time the tween takes to complete")]
        public SharedFloat time;
        [Tooltip("Should the tween smoothly snap all values to integters?")]
        public SharedBool snap;
        [SharedRequired]
        [Tooltip("The stored tweener")]
        public SharedTweener storeTweener;

        private bool complete;

        public override void OnStart()
        {
            var target = GetDefaultGameObject(targetGameObject.Value).transform;
            storeTweener.Value = target.DOLocalMove(to.Value, time.Value, snap.Value);
            storeTweener.Value.OnComplete(() => complete = true);
        }

        public override TaskStatus OnUpdate()
        {
            return complete ? TaskStatus.Success : TaskStatus.Running;
        }

        public override void OnEnd()
        {
            complete = false;
        }

        public override void OnReset()
        {
            to = Vector3.zero;
            time = 0;
            snap = false;
            storeTweener = null;
        }
    }
}