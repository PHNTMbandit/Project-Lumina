namespace ProjectLumina.Player.StateMachine.States
{
    public class GroundedState : State
    {
        protected bool jumpInput;
        protected float lastMoveX, moveInput;

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            jumpInput = stateController.InputReader.JumpInput;
            moveInput = stateController.InputReader.MoveInput;

            stateController.SpriteRenderer.flipX = lastMoveX < 0;

            if (moveInput != 0)
            {
                lastMoveX = moveInput;
            }

            if (jumpInput && stateController.PlayerJump.IsGrounded)
            {
                stateController.ChangeState(stateController.GetState("Jump"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMovement.Move(moveInput);
        }
    }
}