namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCFallState : NPCInAirState
    {
        public NPCFallState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            stateController.CharacterFall.SetGravityScale();
        }
    }
}