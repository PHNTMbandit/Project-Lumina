namespace ProjectLumina.StateMachine.Character.NPC
{
    public abstract class NPCGroundedState : NPCState
    {
        public NPCGroundedState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.Damageable.onDamaged.AddListener(stateController.ChangeToHitState);
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.Damageable.onDamaged.RemoveListener(stateController.ChangeToHitState);
        }
    }
}