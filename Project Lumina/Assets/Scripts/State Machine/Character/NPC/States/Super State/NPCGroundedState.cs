namespace ProjectLumina.StateMachine.Character.NPC
{
    public abstract class NPCGroundedState : NPCState
    {
        public NPCGroundedState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }
    }
}