namespace ProjectLumina.StateMachine.Character.Player
{
    public abstract class PlayerState : State
    {
        protected PlayerStateController stateController;
        protected string stateAnimationName;

        protected PlayerState(PlayerStateController stateController, string stateAnimationName)
        {
            this.stateController = stateController;
            this.stateAnimationName = stateAnimationName;
        }

        public override void OnEnter()
        {
            stateController.Animator.SetBool(stateAnimationName, true);
        }

        public override void OnExit()
        {
            stateController.Animator.SetBool(stateAnimationName, false);
        }

        public override void OnUpdate()
        {
        }

        public override void OnFixedUpdate()
        {
        }
    }
}