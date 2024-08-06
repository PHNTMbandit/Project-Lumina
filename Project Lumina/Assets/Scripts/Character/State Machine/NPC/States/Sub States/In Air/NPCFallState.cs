namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCFallState : NPCInAirState
    {
        public override void OnFixedUpdate(NPCStateController stateController)
        {
            base.OnFixedUpdate(stateController);

            stateController.CharacterFall.SetGravityScale();
        }
    }
}
