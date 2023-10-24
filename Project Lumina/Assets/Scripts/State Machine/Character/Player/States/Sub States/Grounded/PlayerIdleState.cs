namespace ProjectLumina.StateMachine.Character.Player
{
    public class PlayerIdleState : PlayerGroundedState
    {
        public PlayerIdleState(PlayerStateController stateController, string stateAnimationName) : base(stateController, stateAnimationName)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            stateController.InputReader.onAttack = stateController.TryAttack;
        }

        public override void OnExit()
        {
            base.OnExit();

            stateController.InputReader.onAttack -= stateController.TryAttack;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (moveInput.x != 0)
            {
                stateController.StateMachine.ChangeState(stateController.MoveState);
            }
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();

            stateController.CharacterMove.MoveCharacter(moveInput.x);
        }
    }
}