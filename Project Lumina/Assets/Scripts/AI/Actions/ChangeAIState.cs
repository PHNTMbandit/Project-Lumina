using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace ProjectLumina.AI.Actions
{
    [TaskCategory("Animation")]
    public class ChangeAIState : Action
    {
        public SharedString _AIState;

        private CharacterAI _characterAI;

        public override void OnAwake()
        {
            base.OnAwake();

            _characterAI = GetComponent<CharacterAI>();
        }

        public override void OnStart()
        {
            base.OnStart();

            _characterAI.ChangeState(_AIState.Value);
        }
    }
}