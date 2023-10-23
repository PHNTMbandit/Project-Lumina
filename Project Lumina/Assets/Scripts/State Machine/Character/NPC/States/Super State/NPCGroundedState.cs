namespace ProjectLumina.StateMachine.Character.NPC
{
    public abstract class NPCGroundedState : NPCState
    {
        protected float moveInput;

        public NPCGroundedState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (stateController.CharacterFall.IsFalling())
            {
                stateController.StateMachine.ChangeState(stateController.FallState);
            }
        }
    }
}