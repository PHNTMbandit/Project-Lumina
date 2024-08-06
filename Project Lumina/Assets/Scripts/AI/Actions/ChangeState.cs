using BehaviorDesigner.Runtime.Tasks;
using ProjectLumina.StateMachine;
using ProjectLumina.StateMachine.Character.NPC;

namespace ProjectLumina.AI.Conditionals
{
    [TaskCategory("Change State")]
    public class ChangeState : Action
    {
        public NPCState state;

        private NPCStateController _stateController;

        public override void OnAwake()
        {
            base.OnAwake();

            _stateController = GetComponent<NPCStateController>();
        }

        public override void OnStart()
        {
            base.OnStart();

            _stateController.ChangeState(state.name);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}
