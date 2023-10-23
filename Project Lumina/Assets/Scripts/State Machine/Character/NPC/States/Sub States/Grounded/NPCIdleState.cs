namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCIdleState : NPCGroundedState
    {
        public NPCIdleState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }
    }
}