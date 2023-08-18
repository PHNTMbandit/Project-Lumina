using UnityEngine;

namespace ProjectLumina.Player.StateMachine.States
{
    [CreateAssetMenu(fileName = "Wall Slide State", menuName = "Project Lumina/States/Wall Slide State")]
    public class WallSlideState : State
    {
        protected StateController stateController;

        public override void Enter(StateController stateController)
        {
            base.Enter(stateController);

            this.stateController = stateController;

            stateController.InputReader.onJump = TryWallJump;
        }

        public override void Exit(StateController stateController)
        {
            base.Exit(stateController);

            stateController.InputReader.onJump -= TryWallJump;
        }

        public override void LogicUpdate(StateController stateController)
        {
            base.LogicUpdate(stateController);

            if (stateController.PlayerWallSlide.CanWallSlide == false)
            {
                stateController.ChangeState(stateController.GetState("Fall"));
            }
            else if (stateController.PlayerJump.IsGrounded)
            {
                stateController.ChangeState(stateController.GetState("Idle"));
            }
        }

        public override void PhysicsUpdate(StateController stateController)
        {
            base.PhysicsUpdate(stateController);

            stateController.PlayerWallSlide.Slide();
        }

        protected void TryWallJump()
        {
            stateController.PlayerWallSlide.Jump();
            stateController.ChangeState(stateController.GetState("Jump"));
        }
    }
}