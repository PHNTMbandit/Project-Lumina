namespace ProjectLumina.StateMachine.Character.NPC
{
    public abstract class NPCInAirState : NPCState
    {
        public NPCInAirState(NPCStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }
    }
}