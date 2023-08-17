using UnityEngine;

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

            if (stateController.PlayerFall.IsFalling)
            {
                stateController.ChangeState(stateController.GetState("Fall"));
            }
            Debug.Log(jumpInput);

            if (jumpInput && stateController.PlayerJump.CanJump())
            {
                stateController.ChangeState(stateController.GetState("Jump"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerMove.Move(moveInput);
        }
    }
}