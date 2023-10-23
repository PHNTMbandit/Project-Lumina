namespace ProjectLumina.StateMachine.Character.Player
{
    public abstract class PlayerInAirState : PlayerState
    {
        public PlayerInAirState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.InputReader.onDash = stateController.TryDash;
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.InputReader.onDash -= stateController.TryDash;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            stateController.TryWallSlide();
        }
    }
}