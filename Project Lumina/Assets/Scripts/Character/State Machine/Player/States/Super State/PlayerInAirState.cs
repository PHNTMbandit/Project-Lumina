using ProjectLumina.Character;

namespace ProjectLumina.StateMachine.Character.Player
{
    public abstract class PlayerInAirState : PlayerState
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            base.OnEnter(stateController);

            stateController.InputReader.onDash += stateController.Dash;
        }

        public override void OnExit(PlayerStateController stateController)
        {
            base.OnExit(stateController);

            stateController.InputReader.onDash -= stateController.Dash;
        }
    }
}
