namespace ProjectLumina.StateMachine.Character.NPC
{
    public class NPCMoveState : NPCGroundedState
    {
        public NPCMoveState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }
    }
}