namespace ProjectLumina.StateMachine.Character.Player
{
    public abstract class PlayerState : CharacterState<PlayerStateController>
    {
        public override void OnEnter(PlayerStateController stateController)
        {
            stateController.Animator.SetBool(stateAnimationName, true);
        }

        public override void OnExit(PlayerStateController stateController)
        {
            stateController.Animator.SetBool(stateAnimationName, false);
        }

        public override void OnUpdate(PlayerStateController stateController)
        {
            if (stateController.Health.CurrentHealth <= 0)
            {
                stateController.ChangeState("Player Dead State");
            }
        }

        public override void OnFixedUpdate(PlayerStateController stateController) { }
    }
}
