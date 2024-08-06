namespace ProjectLumina.StateMachine.Character.NPC
{
    public abstract class NPCState : CharacterState<NPCStateController>
    {
        public override void OnEnter(NPCStateController stateController)
        {
            stateController.Animator.SetBool(stateAnimationName, true);
        }

        public override void OnExit(NPCStateController stateController)
        {
            stateController.Animator.SetBool(stateAnimationName, false);
        }

        public override void OnUpdate(NPCStateController stateController)
        {
            if (stateController.Health.CurrentHealth <= 0)
            {
                stateController.ChangeState("NPC Dead State");
            }
        }

        public override void OnFixedUpdate(NPCStateController stateController) { }
    }
}
