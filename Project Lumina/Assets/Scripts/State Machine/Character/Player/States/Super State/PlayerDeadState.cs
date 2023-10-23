namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerDeadState : PlayerState
    {
        public PlayerDeadState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }
    }
}