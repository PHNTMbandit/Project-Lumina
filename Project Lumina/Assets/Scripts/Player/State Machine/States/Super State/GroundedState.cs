namespace ProjectLumina.Player.StateMachine.States
{
    public class GroundedState : State
    {
        protected float moveInput;

        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            stateController.InputReader.onJump = delegate { TryJump(stateController); };
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onJump -= delegate { TryJump(stateController); };
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            moveInput = stateController.InputReader.MoveInput;
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(moveInput);
        }

        private void TryJump(StateController stateController)
        {
            if (stateController.PlayerJump.CanJump())
            {
                stateController.ChangeState(stateController.GetState("Jump"));
            }
        }
    }
}